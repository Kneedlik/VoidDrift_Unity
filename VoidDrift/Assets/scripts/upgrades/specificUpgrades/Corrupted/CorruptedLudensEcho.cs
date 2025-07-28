using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedLudensEcho : upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        Type = type.currupted;
        setColor();
    }

    public override void function()
    {
        if (level == 0)
        {
            eventManager.OnImpact += LudensEchoSystem.sharedInstance.CorruptedEchoProc;
            eventManager.SummonOnImpact += LudensEchoSystem.sharedInstance.CorruptedEchoProc;
            description = string.Format("Corrupted Ludens echo Damage + 50 True Damage + 2.5%");
            rarity -= Constants.CorruptedRarityDecrease;
        }else
        {
            LudensEchoSystem.sharedInstance.CFlatDamage += 50;
            LudensEchoSystem.sharedInstance.CTrueDamage += 0.025f;
        }


        level++;
    }
}
