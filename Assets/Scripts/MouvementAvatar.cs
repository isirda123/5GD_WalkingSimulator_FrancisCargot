using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MouvementAvatar : MonoBehaviour
{
    Rigidbody rb;

    [Header("Post Process")]
    [SerializeField] PostProcessVolume processVolume;
    [SerializeField] PostProcessProfile blurryBase, blurryRange;

    [Header("Physics Material")]
    [SerializeField] PhysicMaterial ice;
    [SerializeField] PhysicMaterial snailPhysics;

    [Header ("Other")]
    [SerializeField] float mouvementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 gravityOrientation;
    [SerializeField] float powerOfGravity;
    bool onfloor = false;
    bool modeZoom = false;
    bool sliding = false;

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

        #region InputMvt
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
        #endregion

        #region ModeCarapace
        if (Input.GetKeyDown(KeyCode.Space))
        {
            modeZoom = true;
            processVolume.profile = blurryRange;


        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            modeZoom = false;
            processVolume.profile = blurryBase;
            GetComponent<MeshCollider>().material = snailPhysics;
        }
        #endregion



        if (modeZoom == false)
        {
            
            Moving(moving, rotate);
        }
        else
        {
            print("Zoom In");
            if (moving > 0)
            {
                print("Slide");
                sliding = true;
                GetComponent<MeshCollider>().material = ice;
            }
        }

        
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
        if (sliding == false)
        {
            if (Physics.Raycast(transform.position, -transform.up, out hit, transform.localScale.y * 0.5f + 10f))
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
            }
        }
        else
        {
            rb.AddForce(Physics.gravity);
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
