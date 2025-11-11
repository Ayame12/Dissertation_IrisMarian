using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

public class PlayerAttackManager : MonoBehaviour
{
    private int enemyLayer;
    private int friendlyLayer;

    [Header("Ability 1")]
    public Image ability1Image;
    public Text ability1Text;
    public KeyCode ability1Key;
    public Ability ability1;

    [Header("Ability 2")]
    public Image ability2Image;
    public Text ability2Text;
    public KeyCode ability2Key;
    public Ability ability2;

    [Header("Ability 3")]
    public Image ability3Image;
    public Text ability3Text;
    public KeyCode ability3Key;
    public Ability ability3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMovement pm = GetComponent<PlayerMovement>();
        enemyLayer = pm.enemyLayer;
        friendlyLayer = pm.friendlyLayer;

        ability1Image.fillAmount = 0;
        ability2Image.fillAmount = 0;
        ability3Image.fillAmount = 0;

        ability1Text.text = "";
        ability2Text.text = "";
        ability3Text.text = "";

        ability1.initialize(gameObject);
        ability2.initialize(gameObject);
        ability3.initialize(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(ability1Key) && ability1.isAvailable)
        {
            ability1.action();
        }

        if (Input.GetKeyDown(ability2Key) && ability2.isAvailable)
        {
            ability2.action();
        }

        if (Input.GetKeyDown(ability3Key) && ability3.isAvailable)
        {
            ability3.action();
        }

        if (!ability1.isAvailable)
        {
            ability1Image.fillAmount = ability1.cooldownTimer / ability1.cooldown;
            ability1Text.text = Mathf.Ceil(ability1.cooldownTimer).ToString();
        }
        else
        {
            ability1Text.text = "";
        }

        if (!ability2.isAvailable)
        {
            ability2Image.fillAmount = ability2.cooldownTimer / ability2.cooldown;
            ability2Text.text = Mathf.Ceil(ability2.cooldownTimer).ToString();
        }
        else
        {
            ability2Text.text = "";
        }

        if (!ability3.isAvailable)
        {
            ability3Image.fillAmount = ability3.cooldownTimer / ability3.cooldown;
            ability3Text.text = Mathf.Ceil(ability3.cooldownTimer).ToString();
        }
        else
        {
            ability3Text.text = "";
        }

    }
}
