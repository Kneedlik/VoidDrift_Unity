using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beta : upgrade
{
    public static Beta sharedInstance;
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
        PlayerStats.sharedInstance.increaseAREA(amount);
        Alpha.sharedInstance.rarity += rarityIncrease;
        Gamma.sharedInstance.rarity += rarityIncrease;
        Delta.sharedInstance.rarity += rarityIncrease;
        level++;
    }
}
