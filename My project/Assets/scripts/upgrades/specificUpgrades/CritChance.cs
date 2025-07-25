using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritChance : upgrade
{
    public int amount;
    public static CritChance instance;


    private void Awake()
    {
        if (RuneId == 0)
        {
            instance = this;
        }
        Type = type.red;
        setColor();
    }

    public override void function()
    {
        if(level == 0)
        {
            eventManager.OnCrit += CritSystem.instance.Crit;
            CritSystem.instance.critChance += amount;
        }
        else
        {
            CritSystem.instance.critChance += amount;
        }
        
        level++;
    }
}
