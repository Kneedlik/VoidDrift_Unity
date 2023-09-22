using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_GatlingGun : upgrade
{
    public float ASamount;
    public int DamagePenalty;

    private void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        weapeon gun = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();
        gun.ASmultiplier += ASamount;
        gun.baseDamage -= DamagePenalty;
        
        level++;
    }
}
