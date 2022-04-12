using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float h = 2.0F;
    public float v = 2.0F;

    float yaw = 0f;
    float pitch = 0f;
    bool canPlay;

    [SerializeField]
    GameObject kobraSight, kobraSightCrosshair, ironSight1, ironSight2, redDot, crossHair;
    [SerializeField]
    Animator reload;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (!Cursor.visible)
        {
            yaw += h * Input.GetAxis("Mouse X");
            pitch -= v * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0);
        }

        if (kobraSight.activeInHierarchy && canPlay == true)
        {
            gameObject.GetComponent<Camera>().fieldOfView = 21.9f;
            kobraSightCrosshair.SetActive(true);
            crossHair.SetActive(false);
        }
        else
        {
            gameObject.GetComponent<Camera>().fieldOfView = 50.3f;
            kobraSightCrosshair.SetActive(false);
        }

        if (kobraSight.activeInHierarchy && reload.GetCurrentAnimatorStateInfo(0).IsName("Reload2"))
        {
            canPlay = false;
            print(canPlay);
            gameObject.GetComponent<Camera>().fieldOfView = 50.3f;
        }
        else
        {
            canPlay = true;
        }

        if (ironSight1.activeInHierarchy || ironSight2.activeInHierarchy || redDot.activeInHierarchy || kobraSight.activeInHierarchy)
        {
            crossHair.SetActive(false);
        }
        else
        {
            crossHair.SetActive(true);
        }
    }
}
