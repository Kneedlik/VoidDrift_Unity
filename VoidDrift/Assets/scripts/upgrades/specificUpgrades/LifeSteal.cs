using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSteal : upgrade
{
    public LifeStealSystem system;
    
    void Start()
    {
        system.enabled = false;
        Type = type.red;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (levelingSystem.instance.red >= 5 && levelingSystem.instance.level >= 10)
        {
            return true;
        }
        else return false;
    }

    public override void function()
    {
       if(level == 0)
       {
            system.enabled = true;
            // eventManager.OnDamage += LifeStealSystem.instance.Leech;
            eventManager.OnDamageEnemy += system.Leech;
            description = string.Format("Life leech has 45 seconds coolDown");
       }else if(level == 1)
        {
            LifeStealSystem.instance.coolDown = 45;
            description = string.Format("Life leef has 30 seconds coolDown");
        }else if(level == 2)
        {
            LifeStealSystem.instance.coolDown = 30;
        }

       level++;
    }
}
