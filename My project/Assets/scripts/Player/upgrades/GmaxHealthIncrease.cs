using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MAXHP", menuName = "upgrades/MaxHpIncrease")]
public class GmaxHealthIncrease : upgrade
{
    public static GmaxHealthIncrease sharedInstance;

    GameObject player;
     plaerHealth health;
    public int ammount = 30;


    private void Awake()
    {
        sharedInstance = this;
        Type = type.green;
        setColor();
    }

  public  void increaseHP()
    {
        health.setMaxHP(health.maxHealth + ammount);
        health.increaseHP(health.health + ammount);
    }

    public override void function()
    {
        player = GameObject.FindWithTag("Player");
        health = player.GetComponent<plaerHealth>();
        health.setMaxHP(health.maxHealth + ammount);
        health.increaseHP(health.health + ammount);
        level++;
    }

}
