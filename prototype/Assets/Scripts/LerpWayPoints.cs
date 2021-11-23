using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpWayPoints : MonoBehaviour
{
    public Transform[] waypoints;
    public int waypointIndex;
    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, waypoints[waypointIndex].position, .1f * Time.deltaTime);
        if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 0.05f)
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
    }
}
