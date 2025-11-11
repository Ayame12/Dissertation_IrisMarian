using UnityEngine;

public class MinionRangedCombat : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private float attackRange = 10;
    public float attackDamage = 10;
    public float attackCooldown = 1;

    private float attackTimer;
    private bool isAttacking = false;

    private Stats stats;
    private MinionScript minionScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = GetComponent<Stats>();
        minionScript = GetComponent<MinionScript>();

        attackRange = minionScript.stopDistange;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;

        if(isAttacking)
        {
            if(attackTimer<=0)
            {
                isAttacking = false;
            }
        }

        if ((canAttack()))
        {
            Attack();
        }
    }

    private bool canAttack()
    {
        if(!isAttacking && minionScript.currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, minionScript.currentTarget.transform.position);
            return distanceToTarget <= attackRange;
        }
        return false;
    }

    private void Attack()
    {
        attackTimer = attackCooldown;
        isAttacking = true;

        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        projectile.GetComponent<MinionProjectile>().setTarget(minionScript.currentTarget, attackDamage);

    }
}
