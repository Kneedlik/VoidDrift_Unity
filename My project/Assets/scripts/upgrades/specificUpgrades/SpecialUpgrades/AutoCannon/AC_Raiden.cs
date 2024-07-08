using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Raiden : upgrade
{
    [SerializeField] float DamageMultiplier;
    [SerializeField] int Projectiles;
    [SerializeField] float FireRatePenalty;

    public override bool requirmentsMet()
    {
        if (FinalConditionsMet() && AC_Rockets.instance.level >= 1 && AC_multishot.instance.level >= 1)
        {
            //Debug.Log("je");
            return true;
        }
        else
        {
            //Debug.Log("neni");
            return false;
        }   
    }

    private void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        AutoCannon AC = GameObject.FindWithTag("Weapeon").GetComponent<AutoCannon>();
        AC.damageMultiplier += DamageMultiplier;
        AC.Raiden = true;
        AC.bulletsInABurst = 3;
        AC.rapidFireDelay = 0.1f;
        AC_Rockets.instance.Amount += 2;
        levelingSystem.instance.FinallForm = true;

        PlayerStats.sharedInstance.increaseProjectiles(Projectiles);
        AC_Rockets.instance.SetFirePoints();
        AC.ASmultiplier -= FireRatePenalty;
    }


}
