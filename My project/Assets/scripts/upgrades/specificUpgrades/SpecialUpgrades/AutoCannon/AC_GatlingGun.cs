using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_GatlingGun : upgrade
{
    public static AC_GatlingGun instance;
    public float ASamount;
    public float DamagePenaltyMultiplier;

    private void Start()
    {
        instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        weapeon gun = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();
        gun.ASmultiplier += ASamount;
        gun.damageMultiplier -= DamagePenaltyMultiplier;
        
        level++;
    }
}
