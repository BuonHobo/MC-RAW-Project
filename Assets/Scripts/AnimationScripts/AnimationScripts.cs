using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AnimationScripts : MonoBehaviour
{
    NewPlayerMovement plMov;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirX = 0f;

    // Start is called before the first frame update
    private enum MovementState {idle, running, jumping, falling, sliding};
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        plMov = GetComponent<NewPlayerMovement>();
    }

    void Update()
    {
        dirX = CrossPlatformInputManager.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Horizontal");
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        MovementState state;

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
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        if(plMov.isSliding()){
            state = MovementState.sliding;
        }

        anim.SetInteger("state", (int)state);
    }
}
