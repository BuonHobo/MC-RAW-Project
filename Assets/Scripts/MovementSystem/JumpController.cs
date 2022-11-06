using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class JumpController : MonoBehaviour
{
    PlayerMovement pm;
    ShardController sc;
    Rigidbody2D rb;

    [SerializeField] float jump_force = 30f;
    [SerializeField] float wall_push;
    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<ShardController>();
        pm = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        handleJump();
    }

    void handleJump()
    {
        //Jump and Double/Triple Jump
        if ((CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetButtonDown("Jump")) /*&& isGrounded()*/)
        {
            if (pm.isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jump_force); //cancels vertical speed
            }
            else if (pm.isOnWall())
            {
                rb.velocity = new Vector2(wall_push, jump_force);
            }
            else if (sc.isShardAvailable())
            {
                sc.consumeShard();
                rb.velocity = new Vector2(rb.velocity.x, jump_force); //cancels vertical speed
            }
        }
    }
}