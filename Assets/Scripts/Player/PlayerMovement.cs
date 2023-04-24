using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public Rigidbody2D rb;
    public Animator anim;
    SpriteRenderer spriteRenderer;

    Vector2 movement;
    
    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if(movement != new Vector2(0, 0))
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetInteger("IsAttacking", 0);
            //Invoke("ResetAnimation", 20);
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            anim.SetInteger("IsAttacking", -1);
        }
    }

    void FixedUpdate()
    {
        movement = movement.normalized;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void ResetAnimation(string animName)
    {
        anim.SetBool("IsAttacking", false);
    }
}
