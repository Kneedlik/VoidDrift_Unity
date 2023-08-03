using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detonateShield : upgrade
{
    public int amount;
    void Start()
    {
        Type = type.purple;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (DivineShield.instance.level > 0)
        {
            return true;
        }
        else return false;
    }
    public override void function()
    {
        DivineShieldSystem.instance.damage += amount;
    }
}
