using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class DashController : MonoBehaviour
{
    PlayerMovement p_mov;
    [SerializeField] float dash_force = 35f;
    [SerializeField] public float dash_duration = 0.1f;
    [SerializeField] public float recharge_time = 0.7f;
    [SerializeField] public float dash_cooldown = 0.5f;
    [SerializeField] public int dashes = 1;

    [SerializeField] GameObject canvas;
    Rigidbody2D rb;
    DashIndicator dash_indicator;
    float facing = 1;
    float _recharge_time = 0;
    float _dash_duration = 0;
    float _dash_cooldown = 0;
    int _dashes;
    float gravity;

    public float getRechargeTime()
    {
        return _recharge_time;
    }

    public float getCooldown()
    {
        return _dash_cooldown;
    }

    public int getDashes()
    {
        return _dashes;
    }


    // Start is called before the first frame update
    void Start()
    {
        p_mov = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
        _dashes = dashes;
        dash_indicator=canvas.GetComponent<DashIndicator>();
    }

    bool isDashing()
    {
        return _dash_duration > 0;
    }

    void dash()
    {
        rb.velocity = new Vector2(dash_force * p_mov.getLastFacedDirection(), 0); //Move
        rb.gravityScale = 0; //No gravity
        _dash_duration = dash_duration; //Start dash
        _dashes--; //Consume a dash
        dash_indicator.consumeDash();
    }

    bool buttonWasPressed()
    {
        return CrossPlatformInputManager.GetButton("Dash") || Input.GetKey(KeyCode.LeftShift);
    }

    bool canDash()
    {
        //You can dash if you're not already dashing and you've waited for the cooldown
        return !isDashing() && _dash_cooldown <= 0 && _dashes > 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_dash_duration > 0)
        {
            _dash_duration -= Time.deltaTime;
        }
        else
        {
            rb.gravityScale = gravity; //Resets gravity when dash is over
        }

        if (_dash_cooldown > 0)
        {
            _dash_cooldown -= Time.deltaTime;
        }

        if (_recharge_time > 0)
        {
            _recharge_time -= Time.deltaTime;
        }
        else if (p_mov.isGrounded() && _dashes < dashes)
        {
            _dashes++;
            dash_indicator.restoreDash();

            //Starts cooldown for recharging dashes
            _recharge_time = recharge_time;
        }

        if (buttonWasPressed() && canDash())
        {
            dash(); //Consume a dash

            //Starts cooldown before next dash
            _dash_cooldown = dash_cooldown;
            //Starts cooldown for recharging dashes
            _recharge_time = recharge_time;
        }
    }
}
