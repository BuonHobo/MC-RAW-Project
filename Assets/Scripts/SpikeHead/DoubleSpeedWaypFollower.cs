using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSpeedWaypFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] float GoSpeed = 1f;
    [SerializeField] float ReturnSpeed = 1f;
    [SerializeField] bool go_back = false;
    private bool reversing = false;
    public Vector3 velocity { get; private set; }
    private bool isWaiting = true;
    public Vector3 distance { get; private set; }

    private Animator anim;

    private void updateVelocity()
    {
        distance = waypoints[currentWaypointIndex].transform.position - transform.position;

        if (isWaiting)
        {
            velocity = new Vector3(0, 0, 0);
            if (currentWaypointIndex == 0)
            {
                anim.SetInteger("mov", 1);
            }
        }
        if (currentWaypointIndex == 0 && !isWaiting)
        {
            velocity = distance.normalized * ReturnSpeed;
        }
        if (currentWaypointIndex == 1 && !isWaiting)
        {
            velocity = distance.normalized * GoSpeed;
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        updateVelocity();
    }
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            anim.SetInteger("mov", 0);
            isWaiting = true;
            Invoke("updateWaitingStatus", 1f);
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

    public void updateWaitingStatus()
    {
        isWaiting = false;
    }
}
