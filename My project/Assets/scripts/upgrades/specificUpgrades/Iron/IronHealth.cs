using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronHealth : upgrade
{
    GameObject player;
    plaerHealth health;
    public int amount;
    void Start()
    {
        Type = type.iron;
        setColor();
    }

    public void increaseHP()
    {
        health.setMaxHP(health.maxHealth + amount);
        health.increaseHP(health.health + amount);
    }

    public override void function()
    {
        player = GameObject.FindWithTag("Player");
        health = player.GetComponent<plaerHealth>();
        health.setMaxHP(health.maxHealth + amount);
        health.increaseHP(health.health + amount);
        level++;
    }
}
