using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 1f;
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
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            updateVelocity();
        }
        transform.position += velocity * Time.deltaTime;
    }
}
