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
    public enum JumpInfo
    {
        Wall, //If it's jumping off the wall
        Ground, //If it's jumping off the ground
        Air, //If it's jumping off the air
        None //If it's not jumping
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
        handleJump();
    }

    void handleJump()
    {
        //Jump and Double/Triple Jump
        if ((CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetButtonDown("Jump")) /*&& isGrounded()*/)
        {
            if (pm.isOnGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump_force); //cancels vertical speed
                jump_from = JumpInfo.Ground;
            }
            else if (pm.isFacingWall)
            {
                rb.velocity = new Vector2(wall_push * (-pm.lastFacedDirection), jump_force);
                jump_from = JumpInfo.Wall;
            }
            else if (sc.isShardAvailable())
            {
                sc.consumeShard();
                rb.velocity = new Vector2(rb.velocity.x, jump_force); //cancels vertical speed
                jump_from = JumpInfo.Air;
            }
        }
        else jump_from = JumpInfo.None;
    }
}
