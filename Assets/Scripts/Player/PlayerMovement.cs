using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5;
    public Rigidbody2D rb;
    public Animator anim;
    public float scale = 6;

    SpriteRenderer spriteRenderer;

    public int health = 3;

    [Header("Attacking")]
    public int damage;

    [Header("Attack Combo")]
    public float comboTime = 0.1f;

    [HideInInspector]
    public bool attacking;

    int attackCombo;

    Vector2 movement;

    TrashMonster enemy;
    
    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        gameObject.transform.localScale = new Vector2(scale, scale);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x < 0)
        {
            gameObject.transform.localScale = new Vector2(-scale, transform.localScale.y);
        }
        else if(movement.x > 0)
        {
            gameObject.transform.localScale = new Vector2(scale, transform.localScale.y);
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

    void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TrashMonster trashMonster = collision.gameObject.GetComponent<TrashMonster>();
        if(collision.gameObject.tag == "enemy")
        {
            TakeDamage(trashMonster.damage);

        }
    }
}
