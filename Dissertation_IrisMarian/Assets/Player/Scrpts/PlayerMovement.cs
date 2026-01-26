using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject basicAttackPrefab;
    public float basicAttackCastDuration;
    public float basicAttackCooldown;
    private float basicAttackCooldownTimer;

    //private int groundLayer = 8;
    //private int enemyLayer;
   // private int friendlyLayer;
    private NavMeshAgent agent;
    
    public float rotateSpeedMovement = 0.05f;
    private float rotateVelocity;
    //private float motionSmoothTime = 0.1f;
    public float stopDistance;

    public GameObject targetEnemy;
    
    //private HighLightManager hmScript;

    public GameObject moveIcon;
    public float moveIconTimerMax = 1f;
    private float moveIconTimer;

    public bool hasControl = true;

    private PlayerInput playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        agent.speed = GetComponent<Stats>().speed;

        playerInput = gameObject.GetComponent<PlayerInput>();

        //PlayerScript playerScript = GetComponent<PlayerScript>();
        //groundLayer = playerScript.groundLayer;
        //enemyLayer = playerScript.enemyLayer;
        //friendlyLayer = playerScript.friendlyLayer;
    }

    // Update is called once per frame
    public void frameUpdate()
    {
        agent.speed = GetComponent<Stats>().currentSpeed;

        if(basicAttackCooldownTimer > 0)
        {
            basicAttackCooldownTimer -= Time.deltaTime;
        }

        if (hasControl)
        {
            //if (Input.GetMouseButton(1))
            //{
                //RaycastHit hit;

                //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                //{
                    if (playerInput.move)
                    {
                        moveToPosition(playerInput.mousePos);
                       
                        //move icon
                        Vector3 offset = new Vector3(playerInput.mousePos.x, playerInput.mousePos.y + 0.05f, playerInput.mousePos.z);
                        moveIcon.SetActive(true);
                        moveIcon.transform.position = offset;
                        //moveIcon.GetComponent<Animator>().Play("MoveIconAnim", -1, 0f);

                        moveIconTimer = moveIconTimerMax;
                    }
                    if(playerInput.basicAttack)
                    {
                        targetEnemy = playerInput.targetEnemy;

                        moveToEnemy(targetEnemy);
                    }
                //}
            //}

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

            //if(targetEnemy.GetComponent<PlayerScript>() != null)
            //{
            //    gameObject.GetComponent<PlayerScript>().aggroAllInRange();
            //}
        }
    }
}
