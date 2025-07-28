using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggernaut : upgrade
{
    [SerializeField] int HealthValue;
    [SerializeField] int DamageValue;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.green;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (levelingSystem.instance.green >= 8 && levelingSystem.instance.level >= 15)
        {
            return true;
        }
        else return false;
    }

    public override void function()
    {
        PlayerStats.sharedInstance.increaseDMG(DamageValue);
        GameObject player = GameObject.FindWithTag("Player");
        plaerHealth health = player.GetComponent<plaerHealth>();
        health.setMaxHP(health.maxHealth + HealthValue);
        health.increaseHP(health.health + HealthValue);
        level++;
    }



}
