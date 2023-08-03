using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickRateIncrease : upgrade
{
    [SerializeField] float amount;


    void Start()
    {
        Type = type.blue;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (poisonOnHit.instance.level >= 1 || SpiritFlame.instance.level >= 1)
        {
            return true;
        }
        else return false;
    }

    public override void function()
    {
        PlayerStats.sharedInstance.TickRate -= amount;
        if(PlayerStats.sharedInstance.TickRate < 0.2f)
        {
            PlayerStats.sharedInstance.TickRate = 0.2f;
        }


        level++;
    }

}
