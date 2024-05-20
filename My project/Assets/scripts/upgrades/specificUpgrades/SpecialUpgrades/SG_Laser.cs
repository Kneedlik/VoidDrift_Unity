using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Laser : upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        levelingSystem.instance.FinallForm = true;
        level++;
    }
}
