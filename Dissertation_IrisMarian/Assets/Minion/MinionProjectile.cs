using UnityEngine;

public class MinionProjectile : MonoBehaviour
{
    private GameObject target;
    private float damage;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);

            if (dir.magnitude <= distanceThisFrame)
            {
                damageTarget();
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setTarget(GameObject newTarget, float attackDamage)
    {
        target = newTarget;
        damage = attackDamage;
    }

    private void damageTarget()
    {
        if(target.GetComponent<Stats>())
        {
            target.GetComponent<Stats>().takeDamage(damage);
        }
    }
}
