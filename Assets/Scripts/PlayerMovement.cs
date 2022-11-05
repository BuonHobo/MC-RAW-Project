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
    [SerializeField] float decel = 3f;
    [SerializeField] float max_speed = 15f;
    [SerializeField] float jump_force = 30f;

    Rigidbody2D rb;
    float facing = 1;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        rb = GetComponent<Rigidbody2D>();
    }

    public float getLastFacedDirection()
    {
        return facing;
    }

    //Must be improved
    public bool isGrounded()
    {
        return rb.velocity.y == 0;
    }

    // Update is called once per frame
    void Update()
    {
        //This assumes that you either use keyboard or touch screen
        float dirX = CrossPlatformInputManager.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Horizontal");

        //Saves last faced direction
        if (dirX != 0) { facing = dirX; }


        //Slows player down if he's going too fast
        float new_decel = Mathf.Clamp(Mathf.Abs(rb.velocity.x) - max_speed, 0, decel) * Time.deltaTime * 50;
        if (rb.velocity.x > max_speed)
        {
            rb.velocity += new Vector2(-new_decel * Time.deltaTime * 50, 0);
        }
        else if (rb.velocity.x < -max_speed)
        {
            rb.velocity += new Vector2(+new_decel * Time.deltaTime * 50, 0);
        }

        //This increases player speed so that it doesn't go over the max speed
        float new_accel = Mathf.Clamp(max_speed - Mathf.Abs(rb.velocity.x), 0, accel) * Time.deltaTime * 50;
        rb.velocity += new Vector2(dirX * new_accel, 0);

        //Jump
        if ((CrossPlatformInputManager.GetButton("Jump") || Input.GetButton("Jump")) && isGrounded())
        {
            rb.velocity += new Vector2(0, jump_force);
        }
    }

}
