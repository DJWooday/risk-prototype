using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrappleHook : MonoBehaviour
{
    private Rigidbody rb;
    private bool tethered;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (!tethered) {
            if (collision.rigidbody) {
                FixedJoint j = gameObject.AddComponent<FixedJoint>();
                j.connectedBody = collision.rigidbody;
            } else rb.constraints = RigidbodyConstraints.FreezeAll;

            tethered = true;
        }
    }
}
