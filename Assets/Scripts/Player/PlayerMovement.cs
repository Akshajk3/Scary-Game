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

    [Header("Attack Combo")]
    public float comboTime = 0.1f;

    [HideInInspector]
    public bool attacking;

    int attackCombo;

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
            attackCombo += 1;
            anim.SetInteger("AttackCombo", attackCombo);
            anim.SetBool("IsAttacking", true);
            Invoke("ResetAnimation", 0.2f);
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Invoke("ResetAttackCombo", comboTime);
        }
    }

    void FixedUpdate()
    {
        movement = movement.normalized;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void ResetAnimation()
    {
        anim.SetBool("IsAttacking", false);
    }

    void ResetAttackCombo()
    {
        attackCombo = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {

        }
    }
}
