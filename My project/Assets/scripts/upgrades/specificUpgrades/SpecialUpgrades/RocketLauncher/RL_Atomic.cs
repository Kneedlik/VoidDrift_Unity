using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_Atomic : upgrade
{
    [SerializeField] int DamageAmountMultiplier;
    [SerializeField] float AsPenaltyMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }


    public override void function()
    {
        rocketLauncher RocketLauncher = GameObject.FindWithTag("Weapeon").GetComponent<rocketLauncher>();
        RocketLauncher.Atomic = true;
        RocketLauncher.damageMultiplier += DamageAmountMultiplier;
        RocketLauncher.ASmultiplier -= AsPenaltyMultiplier;
        PlayerStats.sharedInstance.increaseProjectiles(-99);
        level++;
    }
}
