using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class critDamage : upgrade
{
    public int amount;

    private void Awake()
    {
        Type = type.red;
        setColor();
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
        CritSystem.instance.critMultiplier += amount;
        level++;
    }
}
