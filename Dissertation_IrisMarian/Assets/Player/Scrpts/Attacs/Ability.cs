using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Ability : MonoBehaviour
{
    public GameObject player;

    public float cooldown = 5;
    //[HideInInspector]
    public float cooldownTimer = 0;
    [HideInInspector]
    public bool isAvailable = true;
    protected bool isActive = false;

    protected Vector3 initialPosition;
    //[HideInInspector]
    protected Vector3 targetPosition;
    //[HideInInspector]
    //public Vector3 direction;
    //[HideInInspector]
    //public float rot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        if(!isActive && gameObject.GetComponent<MeshRenderer>())
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
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
