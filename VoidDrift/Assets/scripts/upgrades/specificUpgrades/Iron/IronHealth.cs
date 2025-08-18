using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronHealth : upgrade
{
    GameObject player;
    plaerHealth health;
    public float amount;
    void Start()
    {
        Type = type.iron;
        setColor();
    }

    public override void function()
    {
        player = GameObject.FindWithTag("Player");
        health = player.GetComponent<plaerHealth>();
        float HealthTemp = health.baseHealth * amount;
        int TotalHealth = health.maxHealth + (int)HealthTemp;
        health.setMaxHP(TotalHealth);
        health.increaseHP(TotalHealth);
        level++;
    }
}
