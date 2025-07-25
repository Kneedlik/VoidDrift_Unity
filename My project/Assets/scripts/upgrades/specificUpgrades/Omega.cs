using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Omega : upgrade
{
    GameObject player;
    plaerHealth health;
    public int amount;
    public static Omega instance;

    private void Awake()
    {
        instance = this;
        Type = type.blue;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if(Alpha.sharedInstance.level == Alpha.sharedInstance.maxLevel && Beta.sharedInstance.level == Beta.sharedInstance.maxLevel
          &&  Gamma.sharedInstance.level == Gamma.sharedInstance.maxLevel && Delta.sharedInstance.level == Delta.sharedInstance.maxLevel)
        {
            return true;
        }else return false;
    }

    public override void function()
    {
        PlayerStats.sharedInstance.IncreaseAS(amount);
        PlayerStats.sharedInstance.increaseAREA(amount);
        PlayerStats.sharedInstance.increaseDMG(amount);

        player = GameObject.FindWithTag("Player");
        health = player.GetComponent<plaerHealth>();
        float HealthAmount = amount / 100f;
        float HealthTemp = health.baseHealth * HealthAmount;
        int TotalHealth = health.maxHealth + (int)HealthTemp;
        health.setMaxHP(TotalHealth);
        health.increaseHP(TotalHealth);

        level++;
    }
}
