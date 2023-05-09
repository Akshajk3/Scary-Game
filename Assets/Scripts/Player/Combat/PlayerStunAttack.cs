using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunAttack : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<TrashMonster>().TakeStunDamage(damage);
        }
    }
}
