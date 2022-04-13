using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float h = 2.0F;
    public float v = 2.0F;
    [SerializeField]
    float yaw = 0f;
    float pitch = 0f;
    bool canPlay;
    Vector3 FirstPoint;
    Vector3 SecondPoint;
    Vector3 EndPoint;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;
    bool unTouchable = false;
    bool unMoveButton = true;
    public float minClampAngle;
    public float maxClampAngle;
    public bool xAngleClamp;

    [SerializeField]
    GameObject kobraSight, kobraSightCrosshair, ironSight1, ironSight2, redDot, crossHair;
    [SerializeField]
    Animator reload;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        xAngle = 0;
        yAngle = 0;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
    }
    void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            TouchController();
        }

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            MouseController();
        }

        GameObjectsInHierarchy();
    }
    void GameObjectsInHierarchy()
    {
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
    void MouseController()
    {
        if (!Cursor.visible)
        {
            yaw += h * Input.GetAxis("Mouse X");
            pitch -= v * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0);
        }
    }
    void TouchController()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && !unTouchable)
            {
                unTouchable = true;
                unMoveButton = false;
                FirstPoint = Input.GetTouch(0).position;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved && !unMoveButton)
            {
                SecondPoint = Input.GetTouch(0).position;
                xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * -90 / Screen.height;
                yAngle = Mathf.Clamp(yAngle, minClampAngle, maxClampAngle);

                if (xAngleClamp == true)
                {
                    xAngle = Mathf.Clamp(xAngle, -15.56f, 45.6f);
                }
                this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                unTouchable = false;
                unMoveButton = true;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }
        }
    }
}
