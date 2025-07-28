using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferExessDamage : upgrade
{
    
    void Start()
    {
        Type = type.red;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (levelingSystem.instance.level >= 20 && RdamageIncrease.instance.level >= 5)
        {
            return true;
        }
        else return false;
     }  



    public override void function()
    {
        eventManager.PostImpact += DamageTransferManager.instance.TransferDamage;

        level++;
    }
}
