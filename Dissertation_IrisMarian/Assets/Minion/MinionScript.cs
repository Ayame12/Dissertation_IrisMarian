using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

public class MinionScript : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject currentTarget;

    public int enemyLayer;
    public int allyLayer;

    public string enemyTowerTag;
    public string enemyMinionTag;
    public string enemyPlayerTag;

    public float stopDistange;
    public float aggroDistance;
    public float targetSwitchInterval;

    private float targetSwitchTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = GetComponent<Stats>().speed;
        findAndSetTarget();

        targetSwitchTimer = targetSwitchInterval;
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = GetComponent<Stats>().currentSpeed;

        targetSwitchTimer -= Time.deltaTime;

        if(targetSwitchTimer <= 0)
        {
            findAndSetTarget();
        }

        if(currentTarget != null)
        {
            Vector3 directionToTarget = currentTarget.transform.position - gameObject.transform.position;
            Vector3 stoppingPosition = currentTarget.transform.position - directionToTarget.normalized * stopDistange;

            agent.SetDestination(stoppingPosition);

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

        //enemyList.Add(GameObject.FindGameObjectWithTag(enemyPlayerTag));
        //enemyList.Add(GameObject.FindGameObjectWithTag(enemyTowerTag));

        GameObject closestEnemyMinion = getClosestObjectInRadius(enemyMinions, aggroDistance);

        if(closestEnemyMinion != null)
        {
            currentTarget = closestEnemyMinion;
        }
        else
        {
            currentTarget = GameObject.FindGameObjectWithTag(enemyTowerTag);
        }
    }

    private GameObject getClosestObjectInRadius(GameObject[] objects, float radius)
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach(GameObject obj in objects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, obj.transform.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = obj;
            }
        }

        return closestEnemy;
    }
}
