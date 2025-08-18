using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_BugetHoming : upgrade
{
    public int Amount;
    public static RL_BugetHoming instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        rocketLauncher RocketLauncher = GameObject.FindWithTag("Weapeon").GetComponent<rocketLauncher>();
        RocketLauncher.HomingAmount += Amount;
        PlayerStats.sharedInstance.increaseProjectiles(0);
        //RocketLauncher.setFirepoints();

        if(level > 0)
        {
            description = string.Format("Homing Missile + {0}",Amount);
        }
        level++;
    }
}
