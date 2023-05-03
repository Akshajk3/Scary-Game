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
        if(collision.gameObject.tag == "Player")
        {
            trashMonster.sleeping = false;
            trashMonster.canMove = true;
        }
    }
}
