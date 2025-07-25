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
        if (RuneId == 0)
        {
            instance = this;
            Type = type.purple;
            setColor();
            //Debug.Log(222);
        }else
        {
            Type = type.none;
        }
    }

    public override void function()
    {
        if(level == 0)
        {
            if (RuneId == 0)
            {
                devineShield.SetActive(true);
                rarity -= 15;
            }else
            {
                DivineShield.instance.function();
            }
        }else
        {
            DivineShieldSystem.instance.coolDown -= amount;
        }

        level++;
    }
}
