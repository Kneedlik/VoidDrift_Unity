using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritFlame : upgrade
{
    public static SpiritFlame instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Type = type.purple;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (levelingSystem.instance.purple >= 20 && levelingSystem.instance.level >= 40)
        {
            return true;
        }
        else return false;
    }

    public override void function()
    {
        if(level == 0)
        {
            eventManager.OnImpact += SpiritFlameSystem.instance.SpiritFlame;
            eventManager.OnFireAll += SpiritFlameSystem.instance.ChangeColor;
        }
       


        level++;
    }
}
