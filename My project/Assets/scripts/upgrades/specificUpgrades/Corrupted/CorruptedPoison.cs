using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedPoison : upgrade
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
            eventManager.OnImpact += poisonSystem.sharedInstance.CorruptedPoison;
            description = string.Format("Corrupted poison damage + 25 max health damage + 0.1%");
            rarity -= Constants.CorruptedRarityDecrease;
        }else if(level == 1)
        {
            poisonSystem.sharedInstance.CDamage += 25;
            poisonSystem.sharedInstance.CTrueDamage += 0.01f;
        }else if(level == 2)
        {
            poisonSystem.sharedInstance.CDamage += 25;
            poisonSystem.sharedInstance.CTrueDamage += 0.01f;

            rarity = 50;
            description = string.Format("Corrupted poison damage + 20");
        }else
        {
            poisonSystem.sharedInstance.CDamage += 20;
        }
        level++;
    }
}
