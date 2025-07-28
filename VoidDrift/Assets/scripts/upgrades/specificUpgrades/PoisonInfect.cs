using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonInfect : upgrade
{
   
    void Start()
    {
        Type = type.green;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (poisonOnHit.instance.level >= 4)
        {
            return true;
        }
        else return false;
    }

    public override void function()
    {
        if(level == 0)
        {
            poisonSystem.sharedInstance.infect = true;
        }

        level++;
    }

    
}
