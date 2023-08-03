using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleNext : upgrade
{
    
    void Start()
    {
        Type = type.blue;
        setColor();
    }

    public override void function()
    {
        levelingSystem.instance.Double = true;
        level++;
    }
}
