using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockUpgrade : upgrade
{
    
    void Start()
    {
        Type = type.yellow;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if(LightningSystem.instance.shock)
        {
            return true;
        }else return false;
    }

    public override void function()
    {
        if(level == 0)
        {
            description = string.Format("Shocked enemies take + 4 more damage from all sources");
            LightningSystem.instance.armorDamage += 4;
        }else if (level == 1)
        {
            LightningSystem.instance.armorDamage += 6;
        }

        level++;
    }
}
