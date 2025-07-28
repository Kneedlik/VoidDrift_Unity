using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class krakenSlayer : upgrade
{
    public int amount;


    private void Awake()
    {
        Type = type.yellow;
        setColor();
    }

    public override void function()
    {
        if(level == 0)
        {
            eventManager.OnFire += KrakenSlayerSystem.sharedInstance.krakenProc;
            KrakenSlayerSystem.sharedInstance.damage += amount;
        }else if(level == 2)
        {
            KrakenSlayerSystem.sharedInstance.damage += amount;
            //description = string.Format("Repeated triggers of Golden halo on the same enemy deal increased damage up to three times normal damage");
        }
        else if(level == 3)
        {
            KrakenSlayerSystem.sharedInstance.damage += amount;
           // KrakenSlayerSystem.sharedInstance.increaseDamage = true;
            //KrakenSlayerSystem.sharedInstance.max = 3;
        }
        else
        {
            KrakenSlayerSystem.sharedInstance.damage += amount;
        }
       
        level++;
    }
}
