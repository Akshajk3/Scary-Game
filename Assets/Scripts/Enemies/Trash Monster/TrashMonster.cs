using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class TrashMonster : MonoBehaviour
{
    [Header("Movement")]
    public bool sleeping = true;
    public bool canMove = false;
    public Animator anim;
    public AIPath aiPath;
    public SpriteRenderer spriteRenderer;
    public AIDestinationSetter aiDestinationSetter;
    public float moveSpeed = 3f;


    [Header("Health")]
    public int health = 3;

    [Header("Death")]
    public bool dead = false;

    [Header("Attack")]
    public bool canAttack = false;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damageCooldown;
    [SerializeField] private float stunCooldown;
    [SerializeField] public int damage;
    [HideInInspector] public float attackCooldownTimer = Mathf.Infinity;
    [HideInInspector] public float damageCooldownTimer = Mathf.Infinity;
    [HideInInspector] public float stunCooldownTimer = Mathf.Infinity;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    AnimatorClipInfo[] myAnimatorClip;

    void Start()
    {
        aiPath.maxSpeed = moveSpeed;
        sleeping = true;
        canMove = false;
        dead = false;
        AnimatorClipInfo[] myAnimatorClip = anim.GetCurrentAnimatorClipInfo(0);
        attackCooldownTimer = attackCooldown;
        damageCooldownTimer = damageCooldown;
        stunCooldownTimer = stunCooldown;
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    void Update()
    {
        attackCooldownTimer += Time.deltaTime;
        damageCooldownTimer += Time.deltaTime;
        stunCooldownTimer += Time.deltaTime;
        aiPath.canMove = canMove;
        anim.SetBool("IsSleeping", sleeping);

        if(aiPath.desiredVelocity.x >= 0.01f && canMove)
        {
            spriteRenderer.flipX = true;
        }
        else if(aiPath.desiredVelocity.x <= 0.01f && canMove)
        {
            spriteRenderer.flipX = false;
        }

        if (canAttack == true && canMove)
        {
            Attack();
        }

        if (dead == true)
        {
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        if(attackCooldownTimer >= attackCooldown)
        {
            attackCooldownTimer = 0;
            anim.SetTrigger("Attack");
        }
    }

    public void Die()
    { 
        anim.SetTrigger("Dead");
        canMove = false;
    }

    public void TakeDamage(int damage)
    {
        if(damageCooldownTimer >= damageCooldown)
        {
            damageCooldownTimer = 0;
            health -= damage;
            if(health <= 0)
            {
                Die();
            }
        }
    }

    public void TakeStunDamage(int damage)
    {
        if (damageCooldownTimer >= damageCooldown)
        {
            damageCooldownTimer = 0;
            anim.SetTrigger("Damage");

            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }
}
