using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_Plasma : upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (FinalConditionsMet() && RL_StatStick.instance.level >= 1 && RL_Area.Instance.level >= 1)
        {
            return true;
        }
        else return false;
    }

    public override void function()
    {
        rocketLauncher RocketLauncher = GameObject.FindWithTag("Weapeon").GetComponent<rocketLauncher>();
        RocketLauncher.Plasma = true;
        level++;
    }
}
