using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedLightOnHit : upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        Type = type.currupted;
        setColor();
    }

    public override void function()
    {
        if(level == 0)
        {
            eventManager.OnImpact += LightningSystem.instance.CorruptedlightningProc;
            rarity -= Constants.CorruptedRarityDecrease;
            description = string.Format("Red lightning damage + 10 Chance to trigger red lightning + 10%");
        }
        else
        {
            LightningSystem.instance.Cdamage += 10;
            LightningSystem.instance.Cchance += 10;
        }

        level++;
    }
}
