using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCam;
    public int layer = 8;
    public GameObject moveIcon;
    public float moveIconTimerMax = 1f;

    NavMeshAgent navMeshAgent;
    float moveIconTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton((int)MouseButton.Right))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                if(hit.collider.gameObject.layer == layer)
                {
                    if(Input.GetMouseButtonDown(1))
                    {
                        Vector3 offset = new Vector3(hit.point.x, hit.point.y + 0.05f, hit.point.z);
                        //Instantiate(moveIcon, offset, Quaternion.identity);
                        moveIcon.SetActive(true);
                        moveIcon.transform.position = offset;

                        moveIcon.GetComponent<Animator>().Play("MoveIconAnim", -1, 0f);

                        moveIconTimer = moveIconTimerMax;
                    }
                    
                    navMeshAgent.SetDestination(hit.point);
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
