using System.Collections;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;

    public float damageLerpDuration;
    private float currentHealth;
    private float targetHealth;
    private Coroutine damageCoroutine;

    HealthUI healthUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        healthUI = GetComponent<HealthUI>();
        currentHealth = health;
        targetHealth = health;

        healthUI.Start3DSlider(health);

    }

    public void takeDamage(float damage)
    {
        targetHealth -= damage;

        if (targetHealth <= 0 && (gameObject.CompareTag("BlueMinion") || gameObject.CompareTag("RedMinion")))
        {
            Destroy(gameObject);
        }
        else
        {
            startLerpHealth();
        }
    }

    private void startLerpHealth()
    {
        if(damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(lerpHealth());
        }
    }

    private IEnumerator lerpHealth()
    {
        float elapsedTime = 0; 
        float initialHealth = currentHealth;
        float target = targetHealth;

        while(elapsedTime<damageLerpDuration)
        {
            currentHealth = Mathf.Lerp(initialHealth, target, elapsedTime / damageLerpDuration);
            updateHealthUI();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentHealth = target;
        updateHealthUI();

        damageCoroutine = null;
    }

    private void updateHealthUI()
    {
        healthUI.Update3DSlider(currentHealth);
    }
}
