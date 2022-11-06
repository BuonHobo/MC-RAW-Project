using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{

    /*
    These settings work well with
    Mass:1
    Linear Drag:2
    Gravity Scale:10
    */
    [SerializeField] float accel = 3f;

    [SerializeField] float max_speed;
    [SerializeField] float decel;
    [SerializeField] float decel_factor;

    Rigidbody2D rb;
    ShardController sc;
    [HideInInspector]
    public float facing { get; private set; } = 1;
    ContactPoint2D[] contacts;
    bool onWall = false;
    bool onGround = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        rb = GetComponent<Rigidbody2D>();
        sc = GetComponent<ShardController>();
    }

    public bool isGrounded()
    {
        /* contacts = new ContactPoint2D[4];
        rb.GetContacts(contacts);
        foreach (ContactPoint2D contact in contacts)
        {
            if (contact.normal.y > 0.5)
            {
                return true;
            }
        }
        return false; */
        return onGround;
    }

    public bool isOnWall()
    {
        /* contacts = new ContactPoint2D[4];
        rb.GetContacts(contacts);
        foreach (ContactPoint2D contact in contacts)
        {
            if (contact.normal.x == 1 || contact.normal.x == -1)
            {
                return true;
            }
        }
        return false; */
        return onWall;
    }

    public void updateCollisions()
    {
        contacts = new ContactPoint2D[4];
        rb.GetContacts(contacts);
        var tempground = false;
        var tempwall = false;
        foreach (ContactPoint2D contact in contacts)
        {
            Debug.Log(contact.normal);
            if (contact.normal.x == 1 || contact.normal.x == -1)
            {
                tempwall = true;
            }
            if (contact.normal.y > 0.5)
            {
                tempground = true;
            }
        }
        onGround = tempground;
        onWall = tempwall;
    }

    // Update is called once per frame
    void Update()
    {
        updateCollisions();

        //This assumes that you either use keyboard or touch screen
        float dirX = CrossPlatformInputManager.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Horizontal");

        //Saves last faced direction
        if (dirX != 0) { facing = dirX >= 0 ? 1 : -1; }

        float new_accel = Mathf.Clamp(max_speed - Mathf.Abs(rb.velocity.x), 0, accel);

        if (onWall)
        {
            rb.velocity = new Vector2(rb.velocity.x + dirX * new_accel * 50 * Time.deltaTime, 0);
        }
        else
        {
            rb.velocity += new Vector2(dirX * new_accel * 50 * Time.deltaTime, 0);

        }

        slowDown();

    }

    private void slowDown()
    {
        float new_decel = Mathf.Clamp(Mathf.Abs(rb.velocity.x), 0, decel);

        if (Mathf.Abs(rb.velocity.x) > max_speed)
        {
            new_decel *= decel_factor;
        }

        if (rb.velocity.x >= 0)
        {
            rb.velocity -= new Vector2(new_decel * 50 * Time.deltaTime, 0);
        }
        else
        {
            rb.velocity += new Vector2(new_decel * 50 * Time.deltaTime, 0);

        }
    }
}
