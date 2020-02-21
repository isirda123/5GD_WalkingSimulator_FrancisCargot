using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationCamera : MonoBehaviour
{
    [SerializeField] float sensibilityOfMouth;
    public float xRotationMax;
    public float yRotationMax;

    float rotX, rotY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTheMouse();
    }

    void LookAtTheMouse()
    {
        
        rotX += -Input.GetAxis("Mouse Y") * sensibilityOfMouth;
        rotY += Input.GetAxis("Mouse X") * sensibilityOfMouth;
        rotX = Mathf.Clamp(rotX, -xRotationMax, xRotationMax);
        rotY = Mathf.Clamp(rotY, -yRotationMax, yRotationMax);

        transform.localRotation = Quaternion.Euler(rotX, rotY, 0);
        //CheckForRotationMax();
    }

    #region Don't look at this


    void CheckForRotationMax()
    {

        if (transform.localRotation.eulerAngles.x > xRotationMax)
        {
            transform.Rotate(transform.localRotation.eulerAngles.x - xRotationMax, 0, 0);
        }

        if (transform.localRotation.eulerAngles.x < -xRotationMax)
        {
            transform.Rotate(-xRotationMax - transform.localRotation.eulerAngles.x, 0, 0);
        }

        if (transform.localRotation.eulerAngles.y > yRotationMax)
        {
            transform.Rotate(0, transform.localRotation.eulerAngles.x - xRotationMax, 0);
        }

        if (transform.localRotation.eulerAngles.y < -yRotationMax)
        {
            transform.Rotate(0, -yRotationMax - transform.localRotation.eulerAngles.y, 0);
        }

    }
    #endregion
}
