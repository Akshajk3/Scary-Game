using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5;
    public Rigidbody2D rb;
    public Animator anim;
    public float scale = 6;
    public bool canMove = true;

    SpriteRenderer spriteRenderer;

    [Header("Health")]
    public int health = 3;
    public bool death;
    [SerializeField] private float invincibilityTime = 1f;
    [HideInInspector] public float invincibilityTimer = Mathf.Infinity;

    [Header("Attacking")]
    public int damage;
    [SerializeField] private float attackCooldownTime = 1f;
    [HideInInspector] public float attackCooldownTimer = Mathf.Infinity;

    [Header("Attack Combo")]
    public float comboTime = 0.1f;

    [HideInInspector]
    public bool attacking;

    int attackCombo;

    Vector2 movement;

    TrashMonster enemy;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.transform.localScale = new Vector2(scale, scale);
        death = false;
        canMove = true;
        invincibilityTimer = invincibilityTime;
        attackCooldownTimer = attackCooldownTime;
    }

    void Update()
    {
        invincibilityTimer += Time.deltaTime;
        attackCooldownTimer += Time.deltaTime;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x < 0 && canMove == true)
        {
            gameObject.transform.localScale = new Vector2(-scale, transform.localScale.y);
        }
        else if(movement.x > 0 && canMove == true)
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
            Attack();
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Invoke("ResetAttackCombo", comboTime);
        }

        if(death == true)
        {
            anim.enabled = false;
            Invoke("ResetGame", 5f);
        }
    }

    void FixedUpdate()
    {
        if (canMove == true)
        {
            movement = movement.normalized;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    void TakeDamage(int damage)
    {
        if (invincibilityTimer >= invincibilityTime)
        {
            invincibilityTimer = 0;
            anim.SetTrigger("Damage");
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        anim.SetTrigger("Death");
        canMove = false;
    }

    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TrashMonster trashMonster = collision.gameObject.GetComponentInParent<TrashMonster>();
        if(collision.gameObject.tag == "Enemy Hurtbox")
        {
            TakeDamage(trashMonster.damage);
        }
    }
}
