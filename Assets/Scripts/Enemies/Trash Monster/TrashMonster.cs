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


    [Header("Health")]
    public int health = 3;

    [Header("Death")]
    public bool dead = false;
    public GameObject deadMonster;

    [Header("Attack")]
    public bool canAttack = false;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damageCooldown;
    [SerializeField] public int damage;
    [HideInInspector] public float attackCooldownTimer = Mathf.Infinity;
    [HideInInspector] public float damageCooldownTimer = Mathf.Infinity;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    AnimatorClipInfo[] myAnimatorClip;

    void Start()
    {
        sleeping = true;
        canMove = false;
        dead = false;
        AnimatorClipInfo[] myAnimatorClip = anim.GetCurrentAnimatorClipInfo(0);
    }

    void Update()
    {
        attackCooldownTimer += Time.deltaTime;
        damageCooldownTimer += Time.deltaTime;
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

        if (canAttack == true)
        {
            Attack();
        }

        if(dead == true)
        {
            Instantiate(deadMonster, transform.position, Quaternion.identity);
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
            anim.SetTrigger("Damage");
            health -= damage;
            if(health <= 0)
            {
                Die();
            }
        }
    }
}
