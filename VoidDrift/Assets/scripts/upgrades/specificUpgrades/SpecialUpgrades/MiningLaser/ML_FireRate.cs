using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ML_FireRate : upgrade
{
    public static ML_FireRate instance;
    [SerializeField] float AsAmount;
    [SerializeField] int projectiles;
    void Start()
    {
        instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        MiningLaser Laser = GameObject.FindWithTag("Weapeon").GetComponent<MiningLaser>();
        Laser.ASmultiplier += AsAmount;
        PlayerStats.sharedInstance.increaseProjectiles(projectiles);
        level++;
    }
}
