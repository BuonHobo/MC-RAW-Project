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

    private void updateVelocity()
    {
        velocity = waypoints[currentWaypointIndex].transform.position - transform.position;
        velocity = velocity.normalized * speed;
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
        transform.position += velocity * Time.deltaTime;
    }
}
