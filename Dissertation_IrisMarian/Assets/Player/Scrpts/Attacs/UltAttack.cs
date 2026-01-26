using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.Rendering.DebugUI.Table;

public class UltAttack : Ability
{
    public float speed;
    public float range;
    public float lingerDuration;

    private int castNumber = 0;
    private bool isMoving = false;

    private float distance;
    private float lingerTimer;

    public float damage;
    public float slowPercentage;

    private float radius;

    new void Start()
    {
        base.Start();

        radius = GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (isMoving)
        {
            gameObject.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if(isActive)
        {
            lingerTimer -= Time.deltaTime;

            Slow();

            if (lingerTimer <= 0)
            {
                lingerTimer = 0;

                cooldownTimer = cooldown;

                isActive = false;
                if (gameObject.GetComponent<MeshRenderer>())
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
                if (gameObject.GetComponent<SphereCollider>())
                {
                    gameObject.GetComponent<SphereCollider>().enabled = false;
                }
                isMoving = false;

                castNumber = 0;

                Detonate();
            }
        }

        if (Vector3.Distance(initialPosition, gameObject.transform.position) >= distance)
        {
            gameObject.transform.position = targetPosition;
            isMoving = false;
            lingerTimer = lingerDuration;
        }
    }

    public override void action()
    {
        castNumber++;

        if (castNumber == 1)
        {
            isMoving = true;
            isActive = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<SphereCollider>().enabled = true;

            initialPosition = player.GetComponent<Transform>().position;
            targetPosition = getMousePos();

            distance = Vector3.Distance(initialPosition, targetPosition);
            Vector3 direction = (targetPosition - initialPosition).normalized;
            distance = Mathf.Min(distance, range);

            targetPosition = initialPosition + direction * distance;
            targetPosition.y = 1;

            float rot = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            gameObject.SetActive(true);
            gameObject.transform.position = initialPosition;
            gameObject.transform.rotation = Quaternion.Euler(0, rot, 0);
        }
        else if (castNumber == 2)
        {
            lingerTimer = 0;

            cooldownTimer = cooldown;

            isActive = false;
            if (gameObject.GetComponent<MeshRenderer>())
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            if (gameObject.GetComponent<SphereCollider>())
            {
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
            isMoving = false;

            castNumber = 0;

            Detonate();
        }

    }

    private void Detonate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        
        foreach(Collider collider in hitColliders)
        {
            if(collider.gameObject != gameObject)
            {
                if(collider.gameObject.layer == enemyLayer && collider.gameObject.tag != enemyTowerTag)
                {
                    if(collider.gameObject.GetComponent<Stats>())
                    {
                        collider.gameObject.GetComponent<Stats>().takeDamage(damage,1);
                        collider.gameObject.GetComponent<Stats>().removeSlow();
                    }
                }
            }
        }
    }

    private void Slow()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject != gameObject)
            {
                if (collider.gameObject.layer == enemyLayer && collider.gameObject.tag != enemyTowerTag)
                {
                    if (collider.gameObject.GetComponent<Stats>())
                    {
                        collider.gameObject.GetComponent<Stats>().applySlow(slowPercentage, 0.3f);
                    }
                }
            }
        }
    }

    //private void OnTriggerEnter(Collider collider)
    //{
    //    if(collider.gameObject.layer == enemyLayer && collider.gameObject.tag != enemyTowerTag)
    //    {
    //        if (collider.gameObject.GetComponent<Stats>())
    //        {
    //            collider.gameObject.GetComponent<Stats>().applySlow(slowPercentage);
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider collider)
    //{
    //    if (collider.gameObject.layer == enemyLayer && collider.gameObject.tag != enemyTowerTag)
    //    {
    //        if (collider.gameObject.GetComponent<Stats>())
    //        {
    //            collider.gameObject.GetComponent<Stats>().removeSlow();
    //        }
    //    }
    //}
}
