using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeOnHit :upgrade
{
   
    public int damageAmount;
    public int chance;
    public int maxChance;
    public static PrimeOnHit instance;

    void Start()
    {

        instance = this;
        Type = type.red;
        setColor();
    }

    public override void function()
    {
       if(level == 0)
       {
            eventManager.OnImpact += PrimeSystem.instance.prime;
            eventManager.SummonOnImpact += PrimeSystem.instance.prime;
            description = string.Format("Prime damage + {0}% damage Chance to apply prime + {0}%", damageAmount, chance);
        }
        else 
        {
            PrimeSystem.instance.bonusDamage += damageAmount;

            if(PrimeSystem.instance.chance < maxChance)
            {
                PrimeSystem.instance.chance += chance;
            }else
            {
                description = string.Format("Prime damage + {0}%", damageAmount);
            }
            
        }
       level++;
    }
}
