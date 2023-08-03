using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideProjectiles : upgrade
{
    public int amount;
    [SerializeField] int damagePenalty;

    void Start()
    {
        Type = type.yellow;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.increaseSideProjectiles(amount);
        PlayerStats.sharedInstance.increaseDMG(damagePenalty * -1);

        level++;   
    }

}
