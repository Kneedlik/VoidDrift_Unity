using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delta : upgrade
{
    public static Delta sharedInstance;
    GameObject player;
    plaerHealth health;
    public int ammount;
    public int rarityIncrease;


    private void Awake()
    { 
        sharedInstance = this;
        Type = type.blue;
        setColor();
    }

    public override void function()
    {
        player = GameObject.FindWithTag("Player");
        health = player.GetComponent<plaerHealth>();
        health.setMaxHP(health.maxHealth + ammount);
        health.increaseHP(health.health + ammount);

        Beta.sharedInstance.rarity += rarityIncrease;
        Gamma.sharedInstance.rarity += rarityIncrease;
        Alpha.sharedInstance.rarity += rarityIncrease;
        level++;
    }

}
