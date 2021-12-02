using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunTip;
    [SerializeField] private GameObject player;
    [SerializeField] private float bulletForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 force;
            if (Physics.Raycast(ray, out hit)) {
                force = (hit.point - gunTip.position).normalized;
            } else {
                force = gunTip.forward;
            }

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = gunTip.position;
            bullet.transform.rotation = gunTip.rotation;

            bullet.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity;
            bullet.GetComponent<Rigidbody>().AddForce(force * bulletForce, ForceMode.Impulse);
        }
    }
}
