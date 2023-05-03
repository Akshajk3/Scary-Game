using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerMovement player;
    int damage;

    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
        damage = player.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TrashMonster monster = collision.gameObject.GetComponent<TrashMonster>();
            monster.TakeDamage(damage);
        }
    }
}
