using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailAvatar : MonoBehaviour
{
    bool touchingTheFloor = false;
    bool inFakeRotation = false;

    [SerializeField] Vector3 gravityOrientation;
    Vector3 gravityToGive = -Vector3.up * 9.81f;

    RaycastHit previousHit;

    float timerBetweenTwoNormal = 0;


    [SerializeField] Rigidbody rb;
    [SerializeField] Transform downRaycast;
    [SerializeField] float powerOfGravity;
    [SerializeField] float speedOfGravityOrientation;
    [SerializeField] float speedOfSnail;
    [SerializeField] float speedOfSnailRotation;
    [SerializeField] float smoothingTimeBetweenNormals;
    [SerializeField] float fakeSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Z))
        {
            Moving();
        }
        CheckFloor();
        ChangeGravity();
    }

    void ChangeGravity()
    {
        if (inFakeRotation == false)
        {
            rb.AddForce(gravityToGive);
        }
        Debug.DrawLine(transform.position, transform.position + gravityToGive, Color.blue);
    }


    void Moving()
    {
        rb.MovePosition(transform.forward * speedOfSnail * Time.deltaTime + transform.position);
        float rotationOnCamera = Camera.main.transform.localRotation.y * speedOfSnailRotation * Time.deltaTime;
        transform.Rotate(0, rotationOnCamera, 0);
        Camera.main.GetComponent<CameraController>().rotY -= rotationOnCamera;
    }


    void CheckFloor()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, transform.localScale.y + 0.75f))
        {

            Debug.DrawRay(transform.position, -transform.up * 0.75f, Color.yellow);


            gravityOrientation = Vector3.Lerp(gravityOrientation, hit.normal, Time.deltaTime*speedOfGravityOrientation);
            //gravityOrientation = hit.normal;

        }
        else
        {
            print("lol1");
            transform.Rotate(Vector3.right * Time.deltaTime * 10);
            /*gravityOrientation = transform.up;
            print(transform.up);
            gravityToGive = -gravityOrientation * powerOfGravity;
            touchingTheFloor = false;*/

            if (Physics.Raycast(downRaycast.position, -downRaycast.up, out hit, 5f))
            {
                Debug.DrawRay(downRaycast.position, -downRaycast.up * 0.75f, Color.black);
                /*if (Input.GetKey(KeyCode.Z) == false)
                {
                    Moving();
                }
                gravityOrientation = Vector3.Lerp(gravityOrientation, hit.normal, 0.0001f);*/
                if (inFakeRotation == false)
                    StartCoroutine(Fake90());
            }


        }

        gravityToGive = -gravityOrientation * powerOfGravity;

        print(gravityOrientation);
        touchingTheFloor = true;
    }



    IEnumerator Fake90()
    {
        inFakeRotation = true;
        this.GetComponent<MeshCollider>().isTrigger = true;
        for (int i = 0; i < fakeSpeed; i++)
        {
            transform.Rotate(90/fakeSpeed, 0, 0);
            Moving();
            yield return new WaitForSeconds(1/fakeSpeed);
        }

        inFakeRotation = false;
        this.GetComponent<MeshCollider>().isTrigger = false;
    }
        

}
