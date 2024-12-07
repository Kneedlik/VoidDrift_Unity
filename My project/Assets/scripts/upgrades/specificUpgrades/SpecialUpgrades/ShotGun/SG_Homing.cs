using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Homing : upgrade
{
    public int AmmoAmount;
    public float ASAmount;
    public float DamageMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if(FinalConditionsMet() && SG_Damage.instance.level >= 1 && SG_FireRate.instance.level >= 1)
        {
            return true;
        }else return false;
    }

    public override void function()
    {
        projectileShotGun ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        ShotGun.magSize += AmmoAmount;
        ShotGun.ASmultiplier += ASAmount;
        ShotGun.damageMultiplier += DamageMultiplier;
        ShotGun.HomingForm = true;
        levelingSystem.instance.FinallForm = true;
        level++;
    }
}
