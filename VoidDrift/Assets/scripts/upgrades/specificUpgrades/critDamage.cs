using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class critDamage : upgrade
{
    public float amount;
    //float BaseMultiplier;

    private void Awake()
    {
        Type = type.red;
        setColor();
    }

    private void Start()
    {
        //BaseMultiplier = CritSystem.instance.critMultiplier;
    }

    public override bool requirmentsMet()
    {
        if (CritChance.instance.level > 0)
        {
            return true;
        }
        else return false;

    }

    public override void function()
    {
        //float Temp = BaseMultiplier * amount;

        CritSystem.instance.critMultiplier += amount;
        level++;
    }
}
