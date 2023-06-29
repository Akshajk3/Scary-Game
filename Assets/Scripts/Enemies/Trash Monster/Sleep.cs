using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    TrashMonster trashMonster;

    void Start()
    {
        trashMonster = GetComponentInParent<TrashMonster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        if(player.tag == "Player")
        {
            trashMonster.sleeping = false;
            trashMonster.canMove = true;
            trashMonster.aiDestinationSetter.target = player.transform;
        }
    }
}
