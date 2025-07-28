using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSummon : upgrade
{
    [SerializeField] int DamageIncrease;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.none;
    }

    public override void function()
    {
        PlayerStats.sharedInstance.OneSummonBuff = true;
        PlayerStats.sharedInstance.SummonDamage += DamageIncrease;

    }
}
