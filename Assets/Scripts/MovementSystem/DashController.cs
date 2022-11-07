using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class DashController : MonoBehaviour
{
    [SerializeField] float dash_force = 35f;
    [SerializeField] public float dash_duration = 0.1f;

    [SerializeField] public float dash_cooldown = 0.5f;
    NewPlayerMovement p_mov;

    Rigidbody2D rb;
    ShardController sc;
    float gravity;

    public bool isDashing { get; private set; } = false;
    public bool isDashAvailable { get; private set; } = true;


    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<ShardController>();
        rb = GetComponent<Rigidbody2D>();
        p_mov = GetComponent<NewPlayerMovement>();
        gravity = rb.gravityScale;
    }

    void finishDash()
    {
        isDashing = false;
        rb.gravityScale = gravity;
    }

    void restoreDash()
    {
        isDashAvailable = true;
    }

    void dash()
    {
        if (p_mov.isFacingWall)
        {
            rb.velocity = new Vector2(dash_force * (-p_mov.lastFacedDirection), 0);
        } //cancels vertical speed
        else
        {
            rb.velocity = new Vector2(dash_force * p_mov.lastFacedDirection, 0);
        }

        gravity = rb.gravityScale;
        rb.gravityScale = 0; //No gravity

        isDashing = true;
        Invoke("finishDash", dash_duration);

        isDashAvailable = false;
        Invoke("restoreDash", dash_cooldown);

        sc.consumeShard();
    }

    bool buttonIsPressed()
    {
        return CrossPlatformInputManager.GetButtonDown("Fire3") || Input.GetButtonDown("Fire3");
    }

    bool canDash()
    {
        //You can dash if you're not already dashing and you've waited for the cooldown
        return !isDashing && isDashAvailable && sc.isShardAvailable();
    }



    // Update is called once per frame
    void Update()
    {

        if (buttonIsPressed() && canDash())
        {
            dash(); //Consume a dash
        }
    }
}
