using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Laser : upgrade
{
    [SerializeField] int DamageBonus;
    [SerializeField] float FireRatePenalty;

    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        projectileShotGun ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        levelingSystem.instance.FinallForm = true;
        ShotGun.ASmultiplier -= FireRatePenalty;
        ShotGun.baseDamage += DamageBonus;
        level++;
    }
}
