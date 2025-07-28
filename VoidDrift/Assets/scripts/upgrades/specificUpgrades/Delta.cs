using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delta : upgrade
{
    public static Delta sharedInstance;
    GameObject player;
    plaerHealth health;
    public float HealthAmount;
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
        float HealthTemp = health.baseHealth * HealthAmount;
        int TotalHealth = health.maxHealth + (int)HealthTemp;
        health.setMaxHP(TotalHealth);
        health.increaseHP(TotalHealth);

        Beta.sharedInstance.rarity += rarityIncrease;
        Gamma.sharedInstance.rarity += rarityIncrease;
        Alpha.sharedInstance.rarity += rarityIncrease;
        level++;
    }

}
