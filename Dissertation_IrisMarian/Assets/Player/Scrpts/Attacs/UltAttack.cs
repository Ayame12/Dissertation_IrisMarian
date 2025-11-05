using TMPro;
using UnityEngine;
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

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (isMoving)
        {
            gameObject.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if(isActive)
        {
            lingerTimer -= Time.deltaTime;

            if (lingerTimer <= 0)
            {
                lingerTimer = 0;

                cooldownTimer = cooldown;

                isActive = false;
                isMoving = false;

                castNumber = 0;
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

            initialPosition = player.GetComponent<Transform>().position;
            targetPosition = getMousePos();

            distance = Vector3.Distance(initialPosition, targetPosition);
            Vector3 direction = (targetPosition - initialPosition).normalized;
            distance = Mathf.Min(distance, range);

            targetPosition = initialPosition + direction * distance;
            targetPosition.y = 0;

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
            isMoving = false;

            castNumber = 0;
        }

    }
}
