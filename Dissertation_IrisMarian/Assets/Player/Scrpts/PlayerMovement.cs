using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    public int groundLayer = 8;
    
    public float rotateSpeedMovement = 0.05f;
    private float rotateVelocity;
    private float motionSmoothTime = 0.1f;

    public GameObject moveIcon;
    public float moveIconTimerMax = 1f;
    private float moveIconTimer;

    public bool hasControl = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //float speed = agent.velocity.magnitude / agent.speed;
        
        if(hasControl)
        {
            if (Input.GetMouseButton((int)MouseButton.Right))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.layer == groundLayer)
                    {
                        //MOVEMENT
                        agent.SetDestination(hit.point);
                        agent.stoppingDistance = 0;

                        //ROTATION
                        Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));

                        //MOVE iCON
                        Vector3 offset = new Vector3(hit.point.x, hit.point.y + 0.05f, hit.point.z);
                        moveIcon.SetActive(true);
                        moveIcon.transform.position = offset;
                        //moveIcon.GetComponent<Animator>().Play("MoveIconAnim", -1, 0f);

                        moveIconTimer = moveIconTimerMax;
                    }
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
}
