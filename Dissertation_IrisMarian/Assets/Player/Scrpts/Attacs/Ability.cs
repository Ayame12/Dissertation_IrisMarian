using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Ability : MonoBehaviour
{
    public GameObject player;

    PlayerScript playerScript;

    public int enemyLayer;
    public int friendlyLayer;
    public string enemyTowerTag;
    public string enemyPlayerTag;
    public string enemyMinionTag;

    public float cooldown = 5;
    public float cooldownTimer = 0;
    [HideInInspector]
    public bool isAvailable = true;
    protected bool isActive = false;

    protected Vector3 initialPosition;
    protected Vector3 targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();

        enemyLayer = playerScript.enemyLayer;
        friendlyLayer = playerScript.friendlyLayer;

        enemyMinionTag = playerScript.enemyMinionTag;
        enemyPlayerTag = playerScript.enemyPlayerTag;
        enemyTowerTag = playerScript.enemyTowerTag;
    }

    public void Update()
    {
        if (cooldownTimer > 0)
        {
            isAvailable = false;
            cooldownTimer -= Time.deltaTime;
        }
        if(cooldownTimer <= 0)
        {
            isAvailable = true;
            cooldownTimer = 0;
        }

        //if(!isActive)
        //{
        //    if(gameObject.GetComponent<MeshRenderer>())
        //    {
        //        gameObject.GetComponent<MeshRenderer>().enabled = false;
        //    }
        //    if(gameObject.GetComponent<SphereCollider>())
        //    {
        //        gameObject.GetComponent<SphereCollider>().enabled = false;
        //    }
        //}
    }

    // Update is called once per frame
    public virtual void action()
    {
        
    }

    public void initialize(GameObject playerObj)
    {
        player = playerObj;
    }

    public Vector3 getMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            return hit.point;
        }

        return Vector3.zero;
    }
}
