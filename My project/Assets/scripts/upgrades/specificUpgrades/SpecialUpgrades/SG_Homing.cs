using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Homing : upgrade
{
    public int AmmoAmount;
    public float ASAmount;
    public int DamageAmount;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        projectileShotGun ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        ShotGun.magSize += AmmoAmount;
        ShotGun.ASmultiplier += ASAmount;
        ShotGun.baseDamage += DamageAmount;
        ShotGun.HomingForm = true;
        levelingSystem.instance.FinallForm = true;
        level++;
    }
}
