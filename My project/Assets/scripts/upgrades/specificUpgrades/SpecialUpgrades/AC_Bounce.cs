using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Bounce : upgrade
{
    public int Amount;
    //public int DamageAmount;
    public float ProjectileSpeed;

    private void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        AutoCannon AC = GameObject.FindWithTag("Weapeon").GetComponent<AutoCannon>();
        AC.Bounce += Amount;
        AC.BaseForce += ProjectileSpeed;
       // AC.baseDamage += DamageAmount;

      //  PlayerStats.sharedInstance.increaseDMG(0);
        level++;
    }

    
}
