using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHealDamage : upgrade
{
    public static OverHealDamage instance;
   public float multiplier;

    void Start()
    {
        instance = this;
        Type = type.red;
        setColor();
    }

    public override void function()
    {
        if(level == 0)
        {

        }


        level++;
    }
}
