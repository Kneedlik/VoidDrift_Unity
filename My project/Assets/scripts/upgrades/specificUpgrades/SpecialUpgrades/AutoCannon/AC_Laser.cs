using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Laser : upgrade
{
    public float FireRate;

    public override bool requirmentsMet()
    {
        if(FinalConditionsMet() && AC_GatlingGun.instance.level >= 2 )
        {
            return true;
        }else return false;
    }

    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        AutoCannon Gun = GameObject.FindWithTag("Weapeon").GetComponent<AutoCannon>();

        if(level == 0 )
        {
            Gun.Laser = true;
            Gun.ASmultiplier += FireRate;
            levelingSystem.instance.FinallForm = true;
        }

        level++;
    }



}
