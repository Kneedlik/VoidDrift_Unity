using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeDoubleDamage : upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        Type = type.red;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (PrimeOnHit.instance.level >= 5)
        {
            return true;
        }
        else return false;
    }

    public override void function()
    {
        PrimeSystem.instance.multiplier = 2;
        PrimeSystem.instance.speedMultiplier = 1.5f;


        level++;
    }
}
