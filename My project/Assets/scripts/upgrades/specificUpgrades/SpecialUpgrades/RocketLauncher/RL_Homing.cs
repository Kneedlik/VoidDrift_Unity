using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_Homing : upgrade
{
    [SerializeField] int ProjectileAmount;

    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (FinalConditionsMet() && RL_BugetHoming.instance.level >= 2)
        {
            return true;
        }
        else return false;
    }

    public override void function()
    {
        rocketLauncher RocketLauncher = GameObject.FindWithTag("Weapeon").GetComponent<rocketLauncher>();
        RocketLauncher.HomingFinal = true;
        PlayerStats.sharedInstance.increaseProjectiles(ProjectileAmount);
        levelingSystem.instance.FinallForm = true;
        level++;
    }
}
