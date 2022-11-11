using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AnimationScripts : MonoBehaviour
{
    NewPlayerMovement plMov;
    JumpController jmpCtrl;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirX = 0f;
    private DashController dashCtrl;

    // Start is called before the first frame update
    private enum MovementState {idle, running, jumping, falling, sliding, dbjumping, dashing};
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        plMov = GetComponent<NewPlayerMovement>();
        jmpCtrl = GetComponent<JumpController>();
        dashCtrl = GetComponent<DashController>();
    }

    void Update()
    {
        dirX = CrossPlatformInputManager.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Horizontal");
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        MovementState state;

        //Player Running
        if(dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if(dirX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        //Player Idle
        else
        {
            state = MovementState.idle;
        }

        //Jumping Animation Logic
        if(jmpCtrl.jump_from.Equals(JumpController.JumpInfo.Ground))
        {
            state = MovementState.jumping;
        }
        else if(jmpCtrl.jump_from.Equals(JumpController.JumpInfo.Air))
        {
            state = MovementState.dbjumping;
        }
        //End of Jumping Animation Logic

        //Player falling
        //This is needed to start the falling animation when
        //the player is jumping from the ground
        if (rb.velocity.y < -.1f && state != MovementState.dbjumping)
        {
            state = MovementState.falling;
        }
        //Player Sliding
        if(plMov.isSliding()){
            state = MovementState.sliding;
        }

        if(dashCtrl.isDashing){
            state = MovementState.dashing;
        }

        anim.SetInteger("state", (int)state);
    }
}
