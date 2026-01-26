using UnityEngine;

public class TowerProjectileScript : MonoBehaviour
{
    public float speed;
    public float damage;
    private GameObject target;

    bool alreadyHit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.transform.position - gameObject.transform.position;
        float distance = speed * Time.deltaTime;

        if(direction.magnitude < distance)
        {
            hitTarget();
            return;
        }

        gameObject.transform.Translate(direction.normalized * distance, Space.World);
    }

    public void setTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    private void hitTarget()
    {
        if(!alreadyHit)
        { 
            Destroy(gameObject); 

            if(target.GetComponent<Stats>())
            {
                target.GetComponent<Stats>().takeDamage(damage,3);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == target)
        {
            hitTarget();
        }
    }
}
