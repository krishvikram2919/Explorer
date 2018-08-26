using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public static PlayerLook Instance;

    public static bool mActivate = false;
    public Transform playerBody;
    public float mouseSensitivity;

    float xAxisClamp = 0.0f;

    void Awake()
    {
        Instance = this;
        ResetLook();
    }

    void Update()
    {
        if(mActivate)
            RotateCamera();       
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        xAxisClamp -= rotAmountY;

        Vector3 targetRotCam = transform.rotation.eulerAngles;
        Vector3 targetRotBody = playerBody.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;
        targetRotBody.y += rotAmountX;

        if(xAxisClamp > 90)
        {
            xAxisClamp = 90;
            targetRotCam.x = 90;
        }
        else if(xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRotCam.x = 270;
        }

        print(mouseY);


        transform.rotation = Quaternion.Euler(targetRotCam);
        playerBody.rotation = Quaternion.Euler(targetRotBody);


    }


    public void ResetLook()
    {
        mActivate = true;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        playerBody.localRotation = Quaternion.Euler(Vector3.zero);
    }

}
