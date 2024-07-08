using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Laser : upgrade
{
    [SerializeField] float DamageMultiplier;
    [SerializeField] float FireRatePenalty;

    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (FinalConditionsMet() && SG_Damage.instance.level >= 2)
        {
            return true;
        } else return false;
    }

    public override void function()
    {
        projectileShotGun ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        levelingSystem.instance.FinallForm = true;
        ShotGun.ASmultiplier -= FireRatePenalty;
        ShotGun.damageMultiplier += DamageMultiplier;
        level++;
    }
}
