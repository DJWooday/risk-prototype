using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{
    [SerializeField] private GameObject grappleHookPre;
    [SerializeField] private Transform gunTip;
    [SerializeField] private float shotForce = 10;
    private GameObject hook;
    private SpringJoint spring;
    private LineRenderer rope;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            Grapple();
        } else if (Input.GetMouseButtonUp(1)) {
            DeGrapple();
        }

        // Update rope positions
        if (rope) {
            rope.SetPosition(0, gunTip.position);
            rope.SetPosition(1, hook.transform.position);
        }
    }

    private void Grapple() {
        // Instantiate a grappling hook, send it out
        hook = Instantiate(grappleHookPre, gunTip.position, gunTip.rotation);
        hook.GetComponent<Rigidbody>().AddForce(gunTip.forward * shotForce, ForceMode.Impulse);

        // Configure the spring
        spring = transform.root.gameObject.AddComponent<SpringJoint>();
        spring.spring = 40;
        spring.damper = 10;
        spring.autoConfigureConnectedAnchor = false;
        spring.connectedBody = hook.GetComponent<Rigidbody>();
        spring.connectedAnchor = Vector3.zero;
        spring.anchor = gunTip.position - transform.position;

        // Set up the rope
        rope = hook.GetComponent<LineRenderer>();
        rope.enabled = true;
    }
    
    private void DeGrapple() {
        // Remove the hook and the spring joint
        Destroy(hook);
        Destroy(spring);
    }
}
