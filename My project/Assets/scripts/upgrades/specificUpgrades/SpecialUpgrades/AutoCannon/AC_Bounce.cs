using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Bounce : upgrade
{
    public static AC_Bounce instance;
    public int Amount;
    public float DamageMultiplier;
    //public float ProjectileSpeed;

    private void Start()
    {
        instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        AutoCannon AC = GameObject.FindWithTag("Weapeon").GetComponent<AutoCannon>();
        AC.Bounce += Amount;
        //AC.BaseForce += ProjectileSpeed;
        AC.damageMultiplier += DamageMultiplier;

      //  PlayerStats.sharedInstance.increaseDMG(0);
        level++;
    }

    
}
