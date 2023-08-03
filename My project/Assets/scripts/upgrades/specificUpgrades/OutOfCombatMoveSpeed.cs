using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfCombatMoveSpeed : upgrade
{
    [SerializeField] int amount;
   [SerializeField] OutOfCombatMovementSystem system;

    void Start()
    {
        Type = type.blue;
        setColor();
        
        
    }

    // Update is called once per frame
    public override void function()
    {
        if(level == 0)
        {
            system.enabled = true;
            eventManager.OnDamage += system.reduceMoveSpeed;

           system.reset();
            system.speedAmount += amount;
           system.increaseMovespeed();
        }else
        {
            system.reset();
            system.speedAmount += amount;
           system.increaseMovespeed();
        }


        level++;
    }
}
