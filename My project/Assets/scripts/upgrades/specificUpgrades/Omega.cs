using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Omega : upgrade
{
    GameObject player;
    plaerHealth health;
    public int amount;

    private void Awake()
    {
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
        health.setMaxHP(health.maxHealth + amount);
        health.increaseHP(health.health + amount);

        level++;
    }
}
