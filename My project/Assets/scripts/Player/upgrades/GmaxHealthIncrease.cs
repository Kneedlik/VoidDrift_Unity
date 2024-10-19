using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MAXHP", menuName = "upgrades/MaxHpIncrease")]
public class GmaxHealthIncrease : upgrade
{
    public static GmaxHealthIncrease sharedInstance;

    GameObject player;
    plaerHealth health;
    public int HealthAmount = 15;
    public int DamageAmount = 10;


    private void Awake()
    {
        sharedInstance = this;
        Type = type.green;
        setColor();
    }

  public  void increaseHP()
    {
        health.setMaxHP(health.maxHealth + HealthAmount);
        health.increaseHP(health.health + HealthAmount);
    }

    public override void function()
    {
        player = GameObject.FindWithTag("Player");
        health = player.GetComponent<plaerHealth>();
        health.setMaxHP(health.maxHealth + HealthAmount);
        health.increaseHP(health.health + HealthAmount);
        PlayerStats.sharedInstance.increaseDMG(DamageAmount);
        level++;
    }

}
