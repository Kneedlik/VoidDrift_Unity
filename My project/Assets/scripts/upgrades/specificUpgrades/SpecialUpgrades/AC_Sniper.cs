using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Sniper : upgrade
{
    public int damageAmount;
    public int ForceAmount;
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

        gun.baseDamage += damageAmount;
        gun.ForceMultiplier += ForceAmount;
        gun.pierce += PierceAmount;
        gun.baseBulletCoolDown -= ASpenalty;

        PlayerStats p = PlayerStats.sharedInstance;

        p.increaseDMG(0);
        p.IncreaseAS(0);

        level++;
    }
}
