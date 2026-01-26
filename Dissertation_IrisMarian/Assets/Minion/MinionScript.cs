using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

public class MinionScript : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject currentTarget;

    public int enemyLayer;
    public int allyLayer;

    //public string friendlyPlayerTag;
    public string enemyTowerTag;
    public string enemyMinionTag;
    public string enemyPlayerTag;

    public float stopDistange;
    public float aggroDistance;
    public float targetSwitchInterval;

    public float targetSwitchTimer;

    public float CSTimerThreshhold;
    private float timerSincePlayerDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = GetComponent<Stats>().speed;
        findAndSetTarget();

        targetSwitchTimer = 0;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    agent.speed = GetComponent<Stats>().currentSpeed;


    //    if(currentTarget == null)
    //    {
    //        findAndSetTarget();
    //    }

    //    if(currentTarget != null)
    //    {
    //        Vector3 directionToTarget = currentTarget.transform.position - gameObject.transform.position;
    //        Vector3 stoppingPosition = currentTarget.transform.position - directionToTarget.normalized * stopDistange;

    //        agent.SetDestination(stoppingPosition);

    //        faceTarget();
    //    }
    //}

    void Update()
    {
        agent.speed = GetComponent<Stats>().currentSpeed;

        targetSwitchTimer -= Time.deltaTime;

        if (targetSwitchTimer <= 0 || currentTarget == null)
        {
            findAndSetTarget();
        }

        if (currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(gameObject.transform.position, currentTarget.transform.position);

            if(distanceToTarget > stopDistange)
            {
                Vector3 directionToTarget = currentTarget.transform.position - gameObject.transform.position;
                Vector3 stoppingPosition = currentTarget.transform.position - directionToTarget.normalized * stopDistange;

                agent.SetDestination(stoppingPosition);
            }

            faceTarget();
        }
    }

    private void faceTarget()
    {
        Vector3 directionToTarget = (currentTarget.transform.position - gameObject.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*5f);
    }

    private void findAndSetTarget()
    {
        GameObject[] enemyMinions = GameObject.FindGameObjectsWithTag(enemyMinionTag);

        GameObject closestEnemyMinion = getClosestObjectInRadius(enemyMinions);

        if(closestEnemyMinion != null)
        {
            currentTarget = closestEnemyMinion;
        }
        else
        {
            GameObject enemyTower = GameObject.FindGameObjectWithTag(enemyTowerTag);
            float distanceToTower = Vector3.Distance(gameObject.transform.position, enemyTower.transform.position);

            if(distanceToTower < aggroDistance)
            {
                currentTarget = enemyTower;
            }
            else
            {
                GameObject enemyPlayer = GameObject.FindGameObjectWithTag(enemyPlayerTag);
                if(enemyPlayer != null)
                {
                    float distanceToPlayer = Vector3.Distance(gameObject.transform.position, enemyPlayer.transform.position);

                    if (distanceToPlayer < aggroDistance)
                    {
                        currentTarget = enemyPlayer;
                    }
                    else
                    {
                        currentTarget = enemyTower;
                    }
                }
                else
                {
                    currentTarget = enemyTower;
                }
            }
        }
    }

    private GameObject getClosestObjectInRadius(GameObject[] objects)
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach(GameObject obj in objects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, obj.transform.position);

            if(distance < closestDistance && distance < aggroDistance)
            {
                closestDistance = distance;
                closestEnemy = obj;
            }
        }

        return closestEnemy;
    }
}
