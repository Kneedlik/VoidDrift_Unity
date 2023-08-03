using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostExplosoion : upgrade
{
   public static FrostExplosoion instance;

    void Start()
    {
        instance = this;
        Type = type.blue;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (BrittleSystem.Instance.explode == true)
        {
            return true;
        }
        else return false;
    }

    public override void function()
    {
         BrittleSystem.Instance.explodeDamage += 10;
        BrittleSystem.Instance.explodeRadius += 0.15f;
        
        level++;
    }
}
