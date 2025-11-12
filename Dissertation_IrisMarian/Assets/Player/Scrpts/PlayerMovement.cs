using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject basicAttackPrefab;
    public float basicAttackCastDuration;
    public float basicAttackCooldown;

    private float basicAttackCooldownTimer;

    public int enemyLayer;
    public int friendlyLayer;

    private NavMeshAgent agent;

    public int groundLayer = 8;
    
    public float rotateSpeedMovement = 0.05f;
    private float rotateVelocity;
    //private float motionSmoothTime = 0.1f;

    public GameObject targetEnemy;
    public float stopDistance;

    //private HighLightManager hmScript;

    public GameObject moveIcon;
    public float moveIconTimerMax = 1f;
    private float moveIconTimer;

    public bool hasControl = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        agent.speed = GetComponent<Stats>().speed;

        //hmScript = GetComponent<HighLightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = GetComponent<Stats>().currentSpeed;

        if(basicAttackCooldownTimer > 0)
        {
            basicAttackCooldownTimer -= Time.deltaTime;
        }

        if (hasControl)
        {
            if (Input.GetMouseButton(1))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.layer == groundLayer)
                    {
                        moveToPosition(hit.point);
                       
                        //MOVE iCON
                        Vector3 offset = new Vector3(hit.point.x, hit.point.y + 0.05f, hit.point.z);
                        moveIcon.SetActive(true);
                        moveIcon.transform.position = offset;
                        //moveIcon.GetComponent<Animator>().Play("MoveIconAnim", -1, 0f);

                        moveIconTimer = moveIconTimerMax;
                    }
                    else if(hit.collider.gameObject.layer == enemyLayer)
                    {
                        if (hit.transform.parent)
                        {
                            targetEnemy = hit.transform.parent.gameObject;
                        }
                        else if(hit.collider.gameObject != null)
                        {
                            targetEnemy = hit.transform.gameObject;
                        }

                        moveToEnemy(targetEnemy);
                    }
                }
            }

            if(targetEnemy != null)
            { float dis = Vector3.Distance(transform.position, targetEnemy.transform.position);
                if (dis < stopDistance + 1.5f)
                {
                    castBasicAttack();
                    agent.SetDestination(transform.position);
                }
                else
                {
                    agent.SetDestination(targetEnemy.transform.position);
                }
                
            }
        }

        if(moveIconTimer > 0f)
        {
            moveIconTimer -= Time.deltaTime;

            if(moveIconTimer <= 0f)
            {
                moveIcon.SetActive(false);
            }
        }        
    }

    public void moveToPosition(Vector3 position)
    {
        //MOVEMENT
        agent.SetDestination(position);
        agent.stoppingDistance = 0;

        rotateToLookAt(position);

        if(targetEnemy != null)
        {
            //hmScript.deselectHighlight();
            targetEnemy = null;
        }
        
    }

    public void moveToEnemy(GameObject enemy)
    {
        agent.SetDestination(targetEnemy.transform.position);
        agent.stoppingDistance = stopDistance;

        rotateToLookAt(targetEnemy.transform.position);

        //hmScript.selectedHighlight(enemy);
    }

    public void rotateToLookAt(Vector3 lookAtPosition)
    {
        //ROTATION
        Quaternion rotationToLookAt = Quaternion.LookRotation(lookAtPosition - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));

    }

    public void castBasicAttack()
    {
        if(basicAttackCooldownTimer <= 0)
        {
            Stats stats = GetComponent<Stats>();
            stats.applyStun(basicAttackCastDuration);

            basicAttackCooldownTimer = basicAttackCooldown;

            GameObject projectile = Instantiate(basicAttackPrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<MinionProjectile>().setTarget(targetEnemy, stats.damage);
        }
    }
}
