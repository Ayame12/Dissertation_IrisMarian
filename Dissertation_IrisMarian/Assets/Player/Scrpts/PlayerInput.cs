using UnityEngine;
using UnityEngine.AI;

public class PlayerInput : MonoBehaviour
{
    public KeyCode ability1Key;
    public KeyCode ability2Key;
    public KeyCode ability3Key;

    public bool move = false;
    public bool basicAttack = false;
    public bool ability1 = false;
    public bool ability2 = false;
    public bool ability3 = false;

    public Vector3 mousePos;
    public GameObject targetEnemy;

    private int groundLayer = 8;
    private int enemyLayer;
    private int friendlyLayer;
    private NavMeshAgent agent;

    PlayerScript playerScript;

    public bool isAI = false;

    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();

        isAI = playerScript.isAI;

        groundLayer = playerScript.groundLayer;
        enemyLayer = playerScript.enemyLayer;
        friendlyLayer = playerScript.friendlyLayer;

        cam = Camera.main;
    }

    //bool IsOnScreen(Camera cam, Vector3 worldPos)
    //{
    //    Vector3 p = cam.WorldToScreenPoint(worldPos);

    //    return p.z > 0 &&
    //           p.x >= 0 && p.x <= Screen.width &&
    //           p.y >= 0 && p.y <= Screen.height;
    //}

    // Update is called once per frame
    public void frameUpdate()
    {
        if(move)
        {
            move = false;
        }
        if (basicAttack)
        {
            basicAttack = false;
        }
        if(ability1)
        {
            ability1 = false;
        }
        if (ability2)
        {
            ability2 = false;
        }
        if (ability3)
        {
            ability3 = false;
        }

        if(!isAI)
        {
            if (Input.GetMouseButton(1))
            {
                RaycastHit hit;

                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.layer == groundLayer)
                    {
                        move = true;
                    }
                    else if (hit.collider.gameObject.layer == enemyLayer)
                    {
                        basicAttack = true;

                        if (hit.transform.parent)
                        {
                            targetEnemy = hit.transform.parent.gameObject;
                        }
                        else if (hit.collider.gameObject != null)
                        {
                            targetEnemy = hit.transform.gameObject;
                        }
                    }

                    mousePos = hit.point;
                }
            }

            if (Input.GetKeyDown(ability1Key))
            {
                ability1 = true;
            }

            if (Input.GetKeyDown(ability2Key))
            {
                ability2 = true;
            }

            if (Input.GetKeyDown(ability3Key))
            {
                ability3 = true;
            }
        }
    }
}
