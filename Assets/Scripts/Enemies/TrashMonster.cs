using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class TrashMonster : MonoBehaviour
{
    public bool sleeping = true;
    public bool canMove = false;
    public Animator anim;
    public AIPath aiPath;
    public SpriteRenderer spriteRenderer;

    public bool canAttack = false;

    public int health = 3;

    [SerializeField] private float attackCooldown;
    [SerializeField] public int damage;
    [HideInInspector] public float cooldownTimer = Mathf.Infinity;

    void Start()
    {
        sleeping = true;
        canMove = false;
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
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
    }

    public void Attack()
    {
        if(cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            anim.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sleeping = false;
            canMove = true;
        }
    }
}
