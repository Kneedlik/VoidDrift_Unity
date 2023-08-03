using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritChance : upgrade
{
    public int amount;
    public static CritChance instance;


    private void Awake()
    {
        instance = this;
        Type = type.red;
        setColor();
    }

    public override void function()
    {
        if(level == 0)
        {
            eventManager.OnImpact += CritSystem.instance.Crit;  
        }
        else
        {
            CritSystem.instance.critChance += amount;
        }
        
        level++;
    }
}
