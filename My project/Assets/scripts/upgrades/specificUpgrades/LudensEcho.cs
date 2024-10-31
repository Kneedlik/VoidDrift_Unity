using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LudensEcho : upgrade
{
    public static LudensEcho sharedInstance;
    public int amount;
    public int amount2;


    private void Awake()
    {
        sharedInstance = this;
        Type = type.purple;
        setColor();
    }

    public override void function()
    {
        if (level == 0)
        {
            eventManager.OnImpact += LudensEchoSystem.sharedInstance.echoProc;
            eventManager.SummonOnImpact += LudensEchoSystem.sharedInstance.echoProc;
            description = string.Format("First time you damage an enemy deal + {0}% bonus damage", amount);
        }
        else if (level == 1)
        {
            LudensEchoSystem.sharedInstance.damage += amount;
            description = string.Format("Ludens echo now deals damage in an area and has 25 base damage");
        } else if (level == 2)
        {
            LudensEchoSystem.sharedInstance.Aoe = true;
            LudensEchoSystem.sharedInstance.flatDamage += 25;
            description = string.Format("First time you damage an enemy deal + {0}% bonus damage", amount);
        }else if(level == 3)
        {
            LudensEchoSystem.sharedInstance.damage += amount;
            description = string.Format("Ludens echo now also applies spirit flame");
        }
        else if(level == 4)
        {
            description = string.Format("BaseDamage + 15 area + 20%");
            LudensEchoSystem.sharedInstance.burn = true;
            LudensEchoSystem.sharedInstance.burnAmount = 1;
        }
        else if(level == 5)
        {
            LudensEchoSystem.sharedInstance.flatDamage += 15;
            LudensEchoSystem.sharedInstance.aoeSize += 0.2f;
            description = string.Format("First time you damage an enemy deal + {0}% bonus damage", amount2);
        }else if(level == 6)
        {
            LudensEchoSystem.sharedInstance.damage += amount2;
            description = string.Format("Ludens echo base damage + 15 area + 20%");
        }
        else if(level == 7)
        {
            LudensEchoSystem.sharedInstance.flatDamage += 15;
            LudensEchoSystem.sharedInstance.aoeSize += 0.2f;
            description = string.Format("Ludens echo now applyes spirit flame twice");
        }
        else if(level == 8)
        {
            LudensEchoSystem.sharedInstance.burnAmount++;
            rarity -= 25;
            description = string.Format("First time you damage an enemy deal + {0}% bonus damage", amount);
        }
        
        else 
        {
            LudensEchoSystem.sharedInstance.damage += amount;
        }
        level++;

    }
}
