using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondExplosion : upgrade
{
    
    void Start()
    {
        Type = type.blue;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (BrittleOnHit.instance.level >= 6 && FrostExplosoion.instance.level >= 3)
        {
            return true;
        }
        else return false;

       
    }

    public override void function()
    {
        BrittleSystem.Instance.secondExplosion = true;

        level++;
    }
}
