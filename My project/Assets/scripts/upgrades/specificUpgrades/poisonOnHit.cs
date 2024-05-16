using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MAXHP", menuName = "upgrades/PoisonOnHit")]

public class poisonOnHit : upgrade
{
    public static poisonOnHit instance;

    [SerializeField] int damage;
    [SerializeField] int damage2;
    
    [SerializeField] float chance;
    [SerializeField] int maxChance;

    private void Awake()
    {
        Type = type.green;
        setColor();
        instance = this;
    }

    public override void function()
    {
        if(level == 0)
        { 
            eventManager.OnImpact += poisonSystem.sharedInstance.Poison;
            eventManager.SummonOnImpact += poisonSystem.sharedInstance.Poison;

            description = string.Format("Chance to apply poison + {0}% Poison damage + {1}", damage, chance);
        }else if(level == 1)
        {
            poisonSystem.sharedInstance.damage += damage;
            poisonSystem.sharedInstance.chance += chance;
            description = string.Format("Poison coolDown - 0.25 seconds Poison duration + 4 seconds");
        }else if(level == 2)
        {
            poisonSystem.sharedInstance.speed -= 0.25f;
            poisonSystem.sharedInstance.duration += 4;
            description = string.Format("Chance to apply poison + {0}% Poison damage + {1}", damage, chance);
        }
        else if(level == 3)
        {
            poisonSystem.sharedInstance.damage += damage;
            poisonSystem.sharedInstance.chance += chance;
            description = string.Format("Poison now deals extra damage equal to 0.3% of targets max health");
        }
        else if(level == 4)
        {
            poisonSystem.sharedInstance.maxHealthDMG = true;
            description = string.Format("Poison coolDown - 0.2 seconds Poison duration + 4 seconds");
        }else if(level == 5)
        {
            poisonSystem.sharedInstance.speed -= 0.2f;
            poisonSystem.sharedInstance.duration += 4;
            description = string.Format("Poison damage + {0} Chance to apply poison + 20%",damage2);
        }else if(level == 6)
        {
            poisonSystem.sharedInstance.damage += damage2;
            poisonSystem.sharedInstance.chance += 20;
            description = string.Format("Poison damage + {0}", damage2);
        }else if(level == 7)
        {
            poisonSystem.sharedInstance.damage += damage2;
        }
        else if(level == 8)
        {
            poisonSystem.sharedInstance.damage += damage2;
            description = string.Format("Poison max health damage + 0.6%");
        }else if(level == 9)
        {
            poisonSystem.sharedInstance.maxHealthDMGamount += 0.06f;
            description = string.Format("Poison damage + {0}", damage);
            rarity -= 25;
        }else
        {
            poisonSystem.sharedInstance.damage += damage;
            if(poisonSystem.sharedInstance.chance < maxChance)
            {
                poisonSystem.sharedInstance.chance += chance;
            }else
            {
                description = string.Format("Poison damage + {0}", damage);
            }
            
        }
        level++;  
    }

    

}
