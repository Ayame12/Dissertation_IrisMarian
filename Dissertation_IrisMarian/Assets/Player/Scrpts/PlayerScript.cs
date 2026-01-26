using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Android;

public class PlayerScript : MonoBehaviour
{
    public GameObject gameInfo;

    public bool isAI = false;
    public bool isLocal = true;

    public int groundLayer = 8;
    public int enemyLayer;
    public int friendlyLayer;

    public GameObject enemyPlayer;
    public GameObject enemyTower;

    public string enemyTowerTag;
    public string enemyMinionTag;
    public string enemyPlayerTag;

    PlayerInput playerInput;
    PlayerMovement playerMovement;
    PlayerAttackManager playerAttackManager;

    public int creepScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool controllingBlueSide = gameInfo.GetComponent<GameInfo>().isControllingBlueSide;

        if (friendlyLayer == 9 && controllingBlueSide)
        {
            isLocal = true;
        }
        else if(friendlyLayer == 10 && !controllingBlueSide)
        {
            isLocal = true;
        }
        else 
        { 
            isLocal = false; 
        }

        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttackManager = GetComponent<PlayerAttackManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if AI, do AI update here
        //if (isLocal)
        {
            playerInput.frameUpdate();
            playerAttackManager.frameUpdate();
            playerMovement.frameUpdate();
        }
        //else
        {

        }
        
    }

    public void aggroAllInRange()
    {
        GameObject[] enemyMinions = GameObject.FindGameObjectsWithTag(enemyMinionTag);

        if(enemyMinions.Length > 0)
        {
            float minionAggroRange = enemyMinions[0].GetComponent<MinionScript>().aggroDistance;

            foreach(GameObject minion in enemyMinions)
            {
                float distance = Vector3.Distance(gameObject.transform.position, minion.transform.position);

                if (distance < minionAggroRange)
                {
                    minion.GetComponent<MinionScript>().currentTarget = gameObject;
                    minion.GetComponent<MinionScript>().targetSwitchTimer = 3;
                }
            }
        }

        float distanceToTower = Vector3.Distance(gameObject.transform.position, enemyTower.transform.position);
        if (distanceToTower < enemyTower.GetComponent<TowerScript>().range)
        {
            enemyTower.GetComponent<TowerScript>().currentTarget = gameObject;
        }
    }
}
