using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public bool isAI = false;

    public int groundLayer = 8;
    public int enemyLayer;
    public int friendlyLayer;

    public string enemyTowerTag;
    public string enemyMinionTag;
    public string enemyPlayerTag;

    PlayerInput playerInput;
    PlayerMovement playerMovement;
    PlayerAttackManager playerAttackManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttackManager = GetComponent<PlayerAttackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if AI, do AI update here
        playerInput.frameUpdate();
        playerAttackManager.frameUpdate();
        playerMovement.frameUpdate();
    }
}
