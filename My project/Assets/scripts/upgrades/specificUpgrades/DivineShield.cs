using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivineShield : upgrade
{
    public static DivineShield instance;
    public GameObject devineShield;
    public float amount;

    private void Awake()
    {
        instance = this;
        Type = type.purple;
        setColor();
    }

    public override void function()
    {
        if(level == 0)
        {
            devineShield.SetActive(true);
            rarity -= 15;
        }else
        {
            DivineShieldSystem.instance.coolDown -= amount;
        }

        level++;

    }
}
