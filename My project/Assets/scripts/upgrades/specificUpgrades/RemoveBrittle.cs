using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBrittle : upgrade
{
   
    void Start()
    {
        Type = type.yellow;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (FrostExplosoion.instance.level >= 1 && LightningOnHit.instance.level >= 4)
        {
            return true;
        }
        else return false;

    }

    public override void function()
    {
        if(level == 1)
        {
            LightningSystem.instance.removeBrittle = true;
            LightningSystem.instance.removeBrittleChance = 30;
            description = string.Format("Chance to remove brittle on lightning hit + 10%");
        }else
        {
            LightningSystem.instance.removeBrittleChance += 10;
        }

        level++;
    }
}
