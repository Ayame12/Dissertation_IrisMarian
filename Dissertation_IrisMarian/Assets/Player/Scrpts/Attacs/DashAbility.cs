using UnityEngine;
using UnityEngine.AI;

public class DashAbility : Ability
{
    public int terrainLayer = 8;

    public float range;
    public float speed;

    private float distance;

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (isActive)
        {
            player.transform.Translate(0, 0, speed * Time.deltaTime);

            distance = Vector3.Distance(player.transform.position, initialPosition);
            if (distance >= range)
            {
                player.transform.position = targetPosition;

                PlayerMovement movementComp = player.GetComponent<PlayerMovement>();
                movementComp.hasControl = true;

                //gameObject.SetActive(false);
                isActive = false;

                player.GetComponent<NavMeshAgent>().enabled = true;
            }
        }

    }

    public override void action()
    {
        isActive = true;

        cooldownTimer = cooldown;

        initialPosition = player.GetComponent<Transform>().position;
        targetPosition = getMousePos();
        targetPosition.y = initialPosition.y;

        Vector3 direction = (targetPosition - initialPosition).normalized;
        float rot = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        targetPosition = initialPosition + direction * range;

        RaycastHit hit;

        if (Physics.Raycast(initialPosition, direction, out hit, range))
        {
            if (hit.collider.gameObject.layer == terrainLayer)
            {
                float newRange = hit.distance - player.GetComponent<NavMeshAgent>().radius;
                targetPosition = initialPosition + direction * newRange;
            }
        }

        player.GetComponent<Transform>().rotation = Quaternion.Euler(0, rot, 0);

        PlayerMovement movementComp = player.GetComponent<PlayerMovement>();
        movementComp.hasControl = false;

        player.GetComponent<NavMeshAgent>().SetDestination(targetPosition);
        player.GetComponent<NavMeshAgent>().enabled = false;
    }
}
