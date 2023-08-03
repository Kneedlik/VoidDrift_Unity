using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanetPoison : upgrade
{
   
    void Start()
    {
        Type = type.green;
        setColor();
    }

    public override bool requirmentsMet()
    {
       if(poisonOnHit.instance.level >= 8)
       {
            return true;
       }else return false;
    }

    public override void function()
    {
        poisonSystem.sharedInstance.duration = 0;
        level++;
    }
}
