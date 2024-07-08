using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Sniper : upgrade
{
    public float DamageMultiplier;
    public float ForceMultiplier;
    public int PierceAmount;
    public float ASpenalty;

    void Start()
    {
        Type = type.special;
        setColor();

    }

    public override void function()
    {
        weapeon gun = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();

        gun.damageMultiplier += DamageMultiplier;
        gun.ForceMultiplier += ForceMultiplier;
        gun.pierce += PierceAmount;
        gun.baseCoolDown -= ASpenalty;

        PlayerStats p = PlayerStats.sharedInstance;

        p.increaseDMG(0);
        p.IncreaseAS(0);

        level++;
    }
}
