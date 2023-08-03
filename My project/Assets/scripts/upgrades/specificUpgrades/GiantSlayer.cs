using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSlayer : upgrade
{
    
    void Start()
    {
        Type = type.yellow;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if(levelingSystem.instance.level >= 30 && levelingSystem.instance.yellow >= 20)
        {
            return true;
        }else return false; 
    }

    public override void function()
    {
        if(level == 0)
        {
            eventManager.OnImpact += GianSlayerSystem.instance.GiantSlay;
        }else
        {

        }

        level++;
    }
}
