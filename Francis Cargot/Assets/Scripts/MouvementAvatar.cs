using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementAvatar : MonoBehaviour
{
    Rigidbody rb;
    public float mouvementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + 1 * mouvementSpeed * Time.deltaTime));
    }
}
