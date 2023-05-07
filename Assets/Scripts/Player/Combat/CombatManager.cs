using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    [Header("Combat")]
    public bool canReceiveInput;
    public bool inputReceived;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        canReceiveInput = true;
    }

    
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(canReceiveInput)
            {
                inputReceived = true;
                canReceiveInput = false;
            }
            else
            {
                return;
            }
        }
    }

    public void InputManager()
    {
        if(!canReceiveInput)
        {
            canReceiveInput = true;
        }
        else
        {
            canReceiveInput = false;
        }
    }
}
