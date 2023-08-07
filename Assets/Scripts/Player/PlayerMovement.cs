using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Dodge")]
    [SerializeField] float dashPower = 10f;
    [SerializeField] float dashTime = 1f;
    [SerializeField] float dashCooldown = 1f;
    bool isDashing = false;
    bool canDash;


    SpriteRenderer spriteRenderer;

    [Header("Health")]
    public int health = 3;
    public bool death;
    [SerializeField] private float invincibilityTime = 1f;
    [HideInInspector] public float invincibilityTimer = Mathf.Infinity;

    [Header("Combat")]
    public int damage;


    [Header("Attack Combo")]
    public float comboTime = 0.1f;

    [Header("Spawn")]
    public Transform spawnLocation;

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
        canDash = true;
        invincibilityTimer = invincibilityTime;
        gameObject.transform.position = spawnLocation.position;
    }

    void Update()
    {
        if(isDashing)
        {
            return;
        }

        invincibilityTimer += Time.deltaTime;

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

        if(death == true)
        {
            anim.enabled = false;
            Invoke("ResetGame", 5f);
        }

        if(Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }

        if (canMove == true)
        {
            movement = movement.normalized;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        anim.SetTrigger("Dodge");
        rb.velocity = new Vector2(movement.x * dashPower, movement.y * dashPower);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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
