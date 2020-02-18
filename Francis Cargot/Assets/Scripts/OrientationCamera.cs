using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationCamera : MonoBehaviour
{
    Vector2 mouseLastPos;
    Vector2 mousePos;
    [SerializeField] float sensibilityOfMouth;
    [SerializeField] float xRotationMax;
    [SerializeField] float yRotationMax;
    // Start is called before the first frame update
    void Start()
    {
        mouseLastPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTheMouse();
    }

    void LookAtTheMouse()
    {
        mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mouvementOfMouse = mousePos - mouseLastPos;
        this.transform.Rotate(-mouvementOfMouse.y * sensibilityOfMouth, mouvementOfMouse.x * sensibilityOfMouth,0);
        this.transform.rotation = Quaternion.Euler(Mathf.Clamp(this.transform.rotation.eulerAngles.x, -xRotationMax, xRotationMax), 
            Mathf.Clamp(this.transform.rotation.eulerAngles.y, -yRotationMax, yRotationMax), this.transform.rotation.eulerAngles.z);

        mouseLastPos = mousePos;
    }
}
