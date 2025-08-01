using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfCombatMoveSpeed : upgrade
{
    [SerializeField] int amount;
    [SerializeField] int DamageAmount;

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
            OutOfCombatMovementSystem.instance.enabled = true;

            OutOfCombatMovementSystem.instance.reset();
            OutOfCombatMovementSystem.instance.speedAmount += amount;
            OutOfCombatMovementSystem.instance.DamageAmount += DamageAmount;
            OutOfCombatMovementSystem.instance.increaseMovespeed();
        }else
        {
            OutOfCombatMovementSystem.instance.reset();
            OutOfCombatMovementSystem.instance.speedAmount += amount;
            OutOfCombatMovementSystem.instance.DamageAmount += DamageAmount;
            OutOfCombatMovementSystem.instance.increaseMovespeed();
        }

        level++;
    }
}
