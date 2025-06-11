using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TR_Pierce : upgrade
{
    public int PierceIncrease = 1;
    public static TR_Pierce instance;
    void Start()
    {
        Type = type.special;
        setColor();
        instance = this;
    }

    // Update is called once per frame
    public override void function()
    {
        weapeon gun = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();

        gun.pierce += PierceIncrease;
    }
}
