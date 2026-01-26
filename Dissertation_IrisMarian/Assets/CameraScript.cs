using Unity.Cinemachine;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public CinemachineCamera cineCamera;
    public Camera mainCamera;

    private bool usingCineCam = true;

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            usingCineCam = !usingCineCam;

            if(usingCineCam)
            {
                cineCamera.gameObject.SetActive(true);
            }
            else
            {
                cineCamera.gameObject.SetActive(false);
            }
        }

        if(!usingCineCam)
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;

            if(x<10)
            {
                mainCamera.transform.Translate(new Vector3(-1,0,0) * Time.deltaTime * 10, Space.Self);
            }
            else if(x>Screen.width-10)
            {
                mainCamera.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 10, Space.Self);
            }

            if (y < 10)
            {
                mainCamera.transform.Translate(new Vector3(0, -0.7f, -0.3f) * Time.deltaTime * 10, Space.Self);
            }
            else if (y > Screen.width - 10)
            {
                mainCamera.transform.Translate(new Vector3(0, 0.7f, 0.3f) * Time.deltaTime * 10, Space.Self);
            }
        }
    }
}
