using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    public bool isControllingBlueSide = true;

    public GameObject cinemachineCam;

    public GameObject bluePlayer;
    public GameObject redPlayer;

    public Text CSText;
    private PlayerScript localPlayerScript;

    private void Start()
    {
        if (isControllingBlueSide)
        {
            cinemachineCam.GetComponent<CinemachineCamera>().Target.TrackingTarget = bluePlayer.transform;
            localPlayerScript = bluePlayer.GetComponent<PlayerScript>();
        }
        else
        {
            cinemachineCam.GetComponent<CinemachineCamera>().Target.TrackingTarget = redPlayer.transform;
            localPlayerScript = redPlayer.GetComponent<PlayerScript>();
        }
    }

    private void Update()
    {
        CSText.text = "CS: " + localPlayerScript.creepScore.ToString();
    }
}

//enum DamageType { PLAYER, MINION, TOWER };
