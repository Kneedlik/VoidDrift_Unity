using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamma : upgrade
{
    public static Gamma sharedInstance;
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
        PlayerStats.sharedInstance.IncreaseAS(amount);
        Beta.sharedInstance.rarity += rarityIncrease;
        Alpha.sharedInstance.rarity += rarityIncrease;
        Delta.sharedInstance.rarity += rarityIncrease;
        level++;
    }

}
