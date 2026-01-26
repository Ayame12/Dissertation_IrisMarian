using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackManager : MonoBehaviour
{
    private int enemyLayer;
    private int friendlyLayer;

    PlayerInput playerInput;
    PlayerScript playerScript;

    [Header("Ability 1")]
    public Image ability1Image;
    public Text ability1Text;
    private KeyCode ability1Key;
    public Ability ability1;

    [Header("Ability 2")]
    public Image ability2Image;
    public Text ability2Text;
    private KeyCode ability2Key;
    public Ability ability2;

    [Header("Ability 3")]
    public Image ability3Image;
    public Text ability3Text;
    private KeyCode ability3Key;
    public Ability ability3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        enemyLayer = playerScript.enemyLayer;
        friendlyLayer = playerScript.friendlyLayer;

        playerInput = GetComponent<PlayerInput>();

        ability1Image.fillAmount = 0;
        ability2Image.fillAmount = 0;
        ability3Image.fillAmount = 0;

        ability1Text.text = "";
        ability2Text.text = "";
        ability3Text.text = "";


        ability1Key = playerInput.ability1Key;
        ability2Key = playerInput.ability2Key;
        ability3Key = playerInput.ability3Key;

        ability1.initialize(gameObject);
        ability2.initialize(gameObject);
        ability3.initialize(gameObject);
    }

    // Update is called once per frame
    public void frameUpdate()
    {
        if (playerInput.ability1 && ability1.isAvailable)
        {
            ability1.action();
        }

        if (playerInput.ability2 && ability2.isAvailable)
        {
            ability2.action();
        }

        if (playerInput.ability3 && ability3.isAvailable)
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
