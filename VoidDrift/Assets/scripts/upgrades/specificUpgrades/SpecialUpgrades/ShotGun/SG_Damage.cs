using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Damage : upgrade
{
    public static SG_Damage instance;
    public float DamageMultiplier;
    public float ForceMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        projectileShotGun ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        ShotGun.damageMultiplier += DamageMultiplier;
        ShotGun.ForceMultiplier += ForceMultiplier;

        PlayerStats.sharedInstance.increaseDMG(0);
        PlayerStats.sharedInstance.increaseAREA(0);

        level++;
    }
}
