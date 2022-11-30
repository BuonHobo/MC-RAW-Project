using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] float speed = 1f;
    [SerializeField] bool go_back = false;
    private bool reversing = false;
    public Vector3 velocity { get; private set; }
    public Vector3 distance { get; private set; }

    private void updateVelocity()
    {
        distance = waypoints[currentWaypointIndex].transform.position - transform.position;
        velocity = distance.normalized * speed;
    }

    private void Start()
    {
        updateVelocity();
    }
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {

            currentWaypointIndex += reversing ? -1 : +1;

            if (currentWaypointIndex >= waypoints.Length)
            {
                if (go_back)
                {
                    reversing = true;
                    currentWaypointIndex--;
                }
                else
                {
                    currentWaypointIndex = 0;
                }
            }
            else if (currentWaypointIndex <= -1)
            {
                reversing = false;
                currentWaypointIndex++;
            }

        }
        updateVelocity();

        Vector3 displacement = velocity * Time.deltaTime;

        if (displacement.magnitude > distance.magnitude)
        {
            transform.position += distance;
        }
        else
        {
            transform.position += displacement;
        }
    }
}
