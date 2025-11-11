using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public float range;
    public float cooldown;
    public GameObject projectileprefab;
    public Transform spawnPoint;
    public LineRenderer lineRenderer;

    public int enemyLayer;
    public int allyLayer;

    public string enemyTowerTag;
    public string enemyMinionTag;
    public string enemyPlayerTag;

    private float attackTimer;
    private GameObject currentTarget;

    // Update is called once per frame
    void Update()
    {
        if(currentTarget != null && Vector3.Distance(transform.position, currentTarget.transform.position) > range)
        {
            currentTarget = null;
        }

        if(currentTarget == null)
        {
            findAndSetTarget();
        }

        updateLine();

        if(currentTarget != null)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attackCuttentTarget();
                attackTimer = cooldown;
            }
        }
    }

    private void findAndSetTarget()
    {
        GameObject[] enemyMinions = GameObject.FindGameObjectsWithTag(enemyMinionTag);

        //enemyList.Add(GameObject.FindGameObjectWithTag(enemyPlayerTag));
        //enemyList.Add(GameObject.FindGameObjectWithTag(enemyTowerTag));

        GameObject closestEnemyMinion = getClosestObjectInRadius(enemyMinions, range);

        if (closestEnemyMinion != null)
        {
            currentTarget = closestEnemyMinion;
            attackTimer = cooldown;
        }
    }

    private GameObject getClosestObjectInRadius(GameObject[] objects, float radius)
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, obj.transform.position);

            if (distance < closestDistance && distance<=range)
            {
                closestDistance = distance;
                closestEnemy = obj;
            }
        }

        return closestEnemy;
    }

    private void updateLine()
    {
        if(currentTarget != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0,spawnPoint.transform.position);
            lineRenderer.SetPosition(1,currentTarget.transform.position);
        }
        else
        {
            lineRenderer.enabled=false;
        }
    }

    private void attackCuttentTarget()
    {
        GameObject projectile = Instantiate(projectileprefab, spawnPoint.transform.position, Quaternion.identity);
        projectile.GetComponent<TowerProjectileScript>().setTarget(currentTarget);
    }
}
