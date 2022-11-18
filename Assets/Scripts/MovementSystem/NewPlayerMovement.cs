using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask mapLayer;
    [SerializeField] BoxCollider2D wallCheck;
    [SerializeField] float max_sliding_speed;
    [SerializeField] float max_vertical_speed;
    [SerializeField] float running_speed;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    [SerializeField] float coyote_time;
    private Rigidbody2D rb;
    public bool isFacingWall { get; private set; } // Is it facing the wall?
    public bool isOnGround { get; private set; } // Is it on the ground?
    public float lastFacedDirection { get; private set; } // 1 for right, -1 for left
    public bool isOnPlatform { get; private set; } = false;
    private float inputDir;
    private float wallCheck_offset;
    private ContactPoint2D[] contacts = new ContactPoint2D[10];
    private float coyote_cooldown;

    void OnPlatform(bool new_val)
    {
        isOnPlatform = new_val;
    }
    void updateWall()
    {
        isFacingWall = Physics2D.BoxCast(wallCheck.bounds.center, wallCheck.bounds.size, 0, Vector2.right, 0.05f, mapLayer);
    }

    public bool isCoyoteTime()
    {
        return coyote_cooldown > 0;
    }

    void updateCoyote()
    {
        if (isOnGround)
        {
            coyote_cooldown = coyote_time;
        }
        else if (coyote_cooldown > 0)
        {
            coyote_cooldown -= Time.deltaTime;
        }
    }
    void updateGround()
    {
        // isOnGround = Physics2D.BoxCast(groundCheck.bounds.center, groundCheck.bounds.size, 0, Vector2.down, 0.05f, mapLayer);
        var filter = new ContactFilter2D();
        filter.SetLayerMask(mapLayer);
        int i = rb.GetContacts(filter, contacts);
        for (int j = 0; j < i; j++)
        {
            if (contacts[j].normal.y > 0.7)
            {
                isOnGround = true;
                return;
            }
        }
        isOnGround = isOnPlatform;
    }
    void updateInputDirection()
    {
        inputDir = CrossPlatformInputManager.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Horizontal");
    }

    void updateFacedDirection()
    {
        if (inputDir != 0)
        {
            lastFacedDirection = inputDir > 0 ? 1 : -1;
            wallCheck.transform.position = new Vector3(transform.position.x + wallCheck_offset * lastFacedDirection, wallCheck.transform.position.y, 0);
        }
    }

    public bool isSliding()
    {
        return isFacingWall && inputDir != 0;
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wallCheck_offset = wallCheck.transform.localPosition.x;
    }

    void FixedUpdate()
    {
        updateInputDirection();
        updateFacedDirection();
        updateWall();
        updateGround();
        updateCoyote();
    }

    // Update is called once per frame
    void Update()
    {


        float x_velocity = rb.velocity.x + acceleration * inputDir * 50 * Time.deltaTime;
        if (Mathf.Abs(x_velocity) > running_speed)
        {
            x_velocity = rb.velocity.x;
        }
        x_velocity = slowDown(x_velocity);

        float y_velocity = Mathf.Clamp(rb.velocity.y, -max_vertical_speed, max_vertical_speed);
        if (isSliding())
        {
            y_velocity = Mathf.Clamp(y_velocity, -max_sliding_speed, y_velocity);
        }

        rb.velocity = new Vector2(x_velocity, y_velocity);
    }

    private float slowDown(float x_vel)
    {
        float abs = Mathf.Abs(x_vel);
        float new_decel = Mathf.Clamp(abs, 0, deceleration);

        if (abs > running_speed)
        {
            new_decel *= 1 + abs / running_speed;
        }
        if (x_vel >= 0)
        {
            return x_vel - new_decel * 50 * Time.deltaTime;
        }
        {
            return x_vel + new_decel * 50 * Time.deltaTime;
        }
    }
}
