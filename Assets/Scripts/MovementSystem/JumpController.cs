using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class JumpController : MonoBehaviour
{
    private NewPlayerMovement pm;
    private ShardController sc;
    private Rigidbody2D rb;
    [SerializeField] float jump_force = 30f;
    [SerializeField] float wall_push;
    [SerializeField] float jump_cooldown=0.3f;
    [SerializeField] AudioSource jmpSound;
    private float jump_timer=0;
    public enum JumpInfo
    {
        Wall, //If it's jumping off the wall
        Ground, //If it's jumping off the ground
        Air, //If it's jumping off the air
        None, //If it's not jumping
    }
    public JumpInfo jump_from { get; private set; } = JumpInfo.None;


    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<ShardController>();
        pm = GetComponent<NewPlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jump_timer>0){
            jump_timer-=Time.deltaTime;
        }
        handleJump();
    }

    void handleJump()
    {
        //Jump and Double/Triple Jump
        if ((CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetButtonDown("Jump")))
        {
            bool isJumping = false;
            if (pm.isCoyoteTime())
            {
                rb.velocity = new Vector2(rb.velocity.x, jump_force); //cancels vertical speed
                jump_from = JumpInfo.Ground;
                isJumping = true;
            }
            else if (pm.isFacingWall)
            {
                rb.velocity = new Vector2(wall_push * (-pm.lastFacedDirection), jump_force);
                jump_from = JumpInfo.Wall;
                isJumping = true;
            }
            else if (sc.isShardAvailable() && jump_timer<=0)
            {
                sc.consumeShard();
                rb.velocity = new Vector2(rb.velocity.x, jump_force); //cancels vertical speed
                jump_timer=jump_cooldown;
                jump_from = JumpInfo.Air;
                isJumping = true;
            }
            if (isJumping){
                this.jmpSound.Play();
            }
        }
        else if (pm.isOnGround && (rb.velocity.y <= 0 || pm.isOnPlatform) || pm.isSliding())
        {
            jump_from = JumpInfo.None;

        }
    }
}
