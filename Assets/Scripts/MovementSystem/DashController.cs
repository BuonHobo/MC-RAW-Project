using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class DashController : MonoBehaviour
{
    [SerializeField] float dash_force = 35f;
    [SerializeField] public float dash_duration = 0.1f;

    [SerializeField] public float dash_cooldown = 0.5f;
    PlayerMovement p_mov;

    Rigidbody2D rb;
    ShardController sc;
    float _dash_duration = 0;
    float _dash_cooldown = 0;

    float gravity;


    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<ShardController>();
        rb = GetComponent<Rigidbody2D>();
        p_mov = GetComponent<PlayerMovement>();
        gravity = rb.gravityScale;
    }

    bool isDashing()
    {
        return _dash_duration > 0;
    }

    void dash()
    {
        rb.velocity = new Vector2(dash_force * p_mov.facing, 0); //cancels vertical speed
        rb.gravityScale = 0; //No gravity
        sc.consumeShard();
        _dash_duration = dash_duration; //Start dash
    }

    bool buttonIsPressed()
    {
        return CrossPlatformInputManager.GetButtonDown("Fire3") || Input.GetButtonDown("Fire3");
    }

    bool canDash()
    {
        //You can dash if you're not already dashing and you've waited for the cooldown
        return !isDashing() && _dash_cooldown <= 0 && sc.isShardAvailable();
    }

    void handleDashDuration()
    {
        if (_dash_duration > 0)
        {
            _dash_duration -= Time.deltaTime;
        }
        else
        {
            rb.gravityScale = gravity; //Resets gravity when dash is over
        }
    }

    void handleDashCooldown()
    {
        if (_dash_cooldown > 0)
        {
            _dash_cooldown -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleDashDuration();

        handleDashCooldown();

        if (buttonIsPressed() && canDash())
        {
            dash(); //Consume a dash

            //Starts cooldown before next dash
            _dash_cooldown = dash_cooldown;
        }
    }
}
