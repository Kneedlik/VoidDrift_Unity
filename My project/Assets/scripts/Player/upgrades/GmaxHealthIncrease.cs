using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MAXHP", menuName = "upgrades/MaxHpIncrease")]
public class GmaxHealthIncrease : upgrade
{
    public static GmaxHealthIncrease sharedInstance;

    GameObject player;
    plaerHealth health;
    public float HealthAmount = 15;
    public int DamageAmount = 10;


    private void Awake()
    {
        sharedInstance = this;
        Type = type.green;
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
        PlayerStats.sharedInstance.increaseDMG(DamageAmount);
        level++;
    }

}
