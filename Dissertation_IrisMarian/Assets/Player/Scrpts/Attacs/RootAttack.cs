using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RootAttack : Ability
{
    public float speed = 20;
    public float range;
    public float rootDuration;
    public int targetNumber = 2;
    
    private int targetsHit = 0;

    new void Update()
    {
        base.Update();

        if(isActive)
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.transform.Translate(0,0,speed * Time.deltaTime);
            }

            //if(gameObject.GetComponent<Rigidbody>())

            if (targetsHit >= targetNumber)
            {
                isActive = false;
            }

            float distanceTraveled = Vector3.Distance(initialPosition, gameObject.transform.position);

            if (distanceTraveled > range)
            {
                isActive = false;
            }
        }
    }

    public override void action()
    {
        cooldownTimer = cooldown;
        targetsHit = 0;
        isActive = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;

        initialPosition = player.GetComponent<Transform>().position;
        targetPosition = getMousePos();

        Vector3 direction = (targetPosition - initialPosition).normalized;

        float rot = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        gameObject.SetActive(true);
        gameObject.transform.position = initialPosition;
        gameObject.transform.rotation = Quaternion.Euler(0, rot, 0);
    }
}
