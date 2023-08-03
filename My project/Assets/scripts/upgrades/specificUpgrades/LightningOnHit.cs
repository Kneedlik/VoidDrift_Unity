using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightningOnHit : upgrade
{
    public static LightningOnHit instance;
    [SerializeField] int damageAmount;
    [SerializeField] int damageAmount2;
    [SerializeField] int chance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Type = type.yellow;
        setColor();
    }

    public override void function()
    {
        if(level == 0)
        {
            eventManager.OnImpact += LightningSystem.instance.lightningProc;
            description = string.Format("Lightning damage + {0} Chance to trigger Lightning + {0}%", damageAmount,chance);
        }else if(level == 1)
        {
            LightningSystem.instance.damage1 += damageAmount;
            LightningSystem.instance.chance += chance;
            description = string.Format("Lightning has 50% chance to aply shock Shocked enemies take 3 more damage from all sources");
        }else if(level == 2)
        {
            LightningSystem.instance.shock = true;
            LightningSystem.instance.shockChance = 50;
            description = string.Format("Lightning damage + {0} Chance to trigger Lightning + {0}%", damageAmount,chance);
        }else if(level == 3)
        {
            LightningSystem.instance.damage1 += damageAmount;
            LightningSystem.instance.chain += chance;
            description = string.Format("Chance to shock + 15% shock damage + 2");
        }else if(level == 4)
        {
            LightningSystem.instance.shockChance += 15;
            LightningSystem.instance.armorDamage += 2;
            description = string.Format("Lightning damage + {0}", damageAmount);
        }else if(level == 5)
        {
            LightningSystem.instance.damage1 += damageAmount;
            description = string.Format("Chance to shock + 15% shock damage bonus + 3");
        }else if(level == 6)
        {
            LightningSystem.instance.shockChance += 15;
            LightningSystem.instance.armorDamage += 3;
            description = string.Format("Lightning Damage + 5");
        }else if(level == 7)
        {
            LightningSystem.instance.damage1 += 5;
        }
        else if(level == 8)
        {
            LightningSystem.instance.damage1 += 5;
            description = string.Format("Shock damage bonus + 4");
        }else if(level == 9)
        {
            LightningSystem.instance.armorDamage += 4;
            description = string.Format("Lightning Damage + {0}", damageAmount2);
        }else
        {
            description = string.Format("Lightning Damage + {0}", damageAmount2);
        }

        level++;
    }
}
