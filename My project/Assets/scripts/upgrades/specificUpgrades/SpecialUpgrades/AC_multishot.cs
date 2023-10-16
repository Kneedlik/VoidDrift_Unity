using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_multishot : upgrade
{
    public static AC_multishot instance;
    public int multishotAmount;
    public float baseArea;

    void Start()
    {
        instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.increaseProjectiles(multishotAmount);
        weapeon gun = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();

        gun.baseSize += baseArea;
        gun.updateSize(PlayerStats.sharedInstance.areaMultiplier);


        level++;
    }
}
