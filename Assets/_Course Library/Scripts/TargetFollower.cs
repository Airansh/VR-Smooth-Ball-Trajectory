using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;

    private int waypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("waypoints");
        waypoints = new Transform[waypointObjects.Length];
        for (int i = 0; i < waypointObjects.Length; i++)
        {
            waypoints[i] = waypointObjects[i].transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform targetWaypoint = waypoints[waypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Check if the target has reached the waypoint
        if (transform.position == targetWaypoint.position)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }
    }
}
