using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha : upgrade
{
    public static Alpha sharedInstance;
    public int amount;
    public int rarityIncrease;

    private void Awake()
    {
        sharedInstance = this;
        Type = type.blue;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.increaseDMG(amount);
        Beta.sharedInstance.rarity += rarityIncrease;
        Gamma.sharedInstance.rarity += rarityIncrease;
        Delta.sharedInstance.rarity += rarityIncrease;
        level++;

    }
}
