using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMix : upgrade
{
    [SerializeField] int DamageAmount;
    [SerializeField] int SummonDamageAmount;
    [SerializeField] int AsAmount;


    void Start()
    {
        Type = type.blue;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.increaseDMG(DamageAmount);
        PlayerStats.sharedInstance.SummonDamage += SummonDamageAmount;
        PlayerStats.sharedInstance.IncreaseAS(AsAmount);
        level++;
    }
}
