using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementAvatar : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mouvementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 gravityOrientation;
    [SerializeField] float powerOfGravity;
    bool onfloor = false;

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

    private void FixedUpdate()
    {
        CheckForGravity();
    }

    void Moving (float forwardMovement, float rotationMovement)
    {
        if (onfloor == true)
        {
            rb.MovePosition(transform.forward * forwardMovement * Time.deltaTime + transform.position);
            if (forwardMovement > 0)
                transform.Rotate(0, rotationMovement * Time.deltaTime, 0);
        }
    }

    void CheckForGravity()
    {
        RaycastHit hit;

        if (Physics.Raycast (transform.position, -transform.up, out hit, transform.localScale.y*0.5f + 10f))
        {
            
                print("oui");
                Debug.DrawRay(transform.position, -transform.up * 10f, Color.yellow);
                rb.useGravity = false;
                gravityOrientation = hit.normal.normalized;
                if (gravityOrientation.y > 0)
                {
                    rb.AddRelativeForce(0, -gravityOrientation.y * powerOfGravity, 0);

                }
                else
                {
                    rb.AddRelativeForce(0, gravityOrientation.y * powerOfGravity, 0);

                }
        }
        else
        {
            //rb.useGravity = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        onfloor = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        onfloor = false;
    }
}
