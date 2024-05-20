using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Damage : upgrade
{
    public int DamageAmount;
    public float ForceMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        projectileShotGun ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        ShotGun.baseDamage += DamageAmount;
        ShotGun.ForceMultiplier += ForceMultiplier;

        PlayerStats.sharedInstance.increaseDMG(0);
        PlayerStats.sharedInstance.increaseAREA(0);

        level++;
    }
}
