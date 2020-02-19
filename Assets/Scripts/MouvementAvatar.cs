using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementAvatar : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mouvementSpeed;
    [SerializeField] float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moving = 0;
        float rotate = 0;

        if (Input.GetKey(KeyCode.Z))
        {
            print("move");
            moving = mouvementSpeed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            print("rotate-");
            rotate -= rotationSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            print("rotate+");
            rotate += rotationSpeed;
        }

        Moving(moving, rotate);
    }

    void Moving (float forwardMovement, float rotationMovement)
    {
        rb.MovePosition(transform.forward * forwardMovement * Time.deltaTime + transform.position);
        if (forwardMovement > 0)
            transform.Rotate(0, rotationMovement * Time.deltaTime, 0);
    }
}
