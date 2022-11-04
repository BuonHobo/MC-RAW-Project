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
    [SerializeField] float accel=3f;
    [SerializeField] float max_speed=15f;
    [SerializeField] float jump_force=25f;
    [SerializeField] float dash_force_horizontal=35f;
    [SerializeField] float dash_length=0.1f;
    [SerializeField] float dash_cooldown=0.7f;
    Rigidbody2D rb;
    float facing = 1;
    float gravity;
    float _dash_cooldown;
    float _dash_length = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _dash_cooldown = dash_cooldown;
        gravity = rb.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_dash_cooldown > 0)
        {
            _dash_cooldown -= Time.deltaTime;

        }
        if (_dash_length > 0)
        {
            _dash_length -= Time.deltaTime;

        }
        else
        {
            rb.gravityScale = gravity;
            if (rb.velocity.x > max_speed)
            {
                rb.velocity += new Vector2(-accel, 0);
            }
            else if (rb.velocity.x < -max_speed)
            {
                rb.velocity += new Vector2(+accel, 0);
            }
        }

        float dirX = CrossPlatformInputManager.GetAxis("Horizontal");
        if (dirX != 0) { facing = dirX; }



        float new_accel = Mathf.Clamp(max_speed - Mathf.Abs(rb.velocity.x), 0, accel);
        rb.velocity += new Vector2(dirX * new_accel, 0);


        if (CrossPlatformInputManager.GetButton("Jump") && rb.velocity.y == 0)
        {
            rb.velocity += new Vector2(0, jump_force);
        }
        if (CrossPlatformInputManager.GetButton("Dash") && _dash_cooldown <= 0)
        {
            rb.velocity = new Vector2(dash_force_horizontal * facing, 0);
            rb.gravityScale = 0;

            _dash_cooldown = dash_cooldown;
            _dash_length = dash_length;
        }
    }
}
