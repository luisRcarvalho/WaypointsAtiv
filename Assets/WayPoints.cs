using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public GameObject[] waypoints; 
    int currentWP = 0;
    float speed= 1.0f;
    float accuracy= 1.0f;
    float rotSpeed= 0.4f;
    private void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    private void LateUpdate()
    {
        if (waypoints.Length == 0) return;
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y, waypoints[currentWP].transform.position.z);
        Vector3 direction = lookAtGoal - this.transform.position; this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        if (direction.magnitude < accuracy)
        {
            currentWP++;
            if (currentWP >= waypoints.Length)
            { 
                currentWP = 0; 
            }
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}