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

    // Start is called before the first frame update
    private enum MovementState {idle, running, jumping, falling, sliding, dbjumping};
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        plMov = GetComponent<NewPlayerMovement>();
        jmpCtrl = GetComponent<JumpController>();
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
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        //Player Sliding
        if(plMov.isSliding()){
            state = MovementState.sliding;
        }

        anim.SetInteger("state", (int)state);
    }
}
