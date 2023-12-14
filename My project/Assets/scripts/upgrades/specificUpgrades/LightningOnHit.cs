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
            description = string.Format("Lightning damage + {0} Chance to trigger Lightning + {0}%", damageAmount, chance);
        }else if(level == 2)
        {
            LightningSystem.instance.damage1 += damageAmount;
            LightningSystem.instance.chance += chance;
            description = string.Format("Lightning has 40% chance to aply shock Shocked enemies take 4 more damage from all sources");
        }else if(level == 3)
        {
            LightningSystem.instance.shock = true;
            description = string.Format("Chance to shock + 20% Lightning damage + 6");
        }else if(level == 4)
        {
            LightningSystem.instance.shockChance += 15;
            LightningSystem.instance.damage1 += 6;
            description = string.Format("Lightning damage + 10");
        }else if(level == 5)
        {
            LightningSystem.instance.damage1 += 10;
            description = string.Format("Chance to shock + 25% Lightning damage + 6");
        }else if(level == 6)
        {
            LightningSystem.instance.shockChance += 25;
            LightningSystem.instance.armorDamage += 3;
            description = string.Format("Lightning Damage + 12");
        }else if(level == 7)
        {
            LightningSystem.instance.damage1 += 12;
            description = string.Format("Lightning Damage + 15");
        }
        else if(level == 8)
        {
            LightningSystem.instance.damage1 += 15;
            description = string.Format("Lightning Damage + {0}", damageAmount2);
            rarity -= 20;
        }else
        {
            description = string.Format("Lightning Damage + {0}", damageAmount2);
            LightningSystem.instance.damage1 += damageAmount2; 
        }

        level++;
    }
}
