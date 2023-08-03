using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statsOnRevive : upgrade
{
    public static statsOnRevive instance;

    public int amount;
   public int healthAmaount;
    public int fireRateAmount;

    public int health;
    public int damage;
    public int fireRate;


    void Start()
    {
        instance = this;
        Type = type.purple;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if(Revive.Instance.level >= Revive.Instance.maxLevel)
        {
            return true;
        }else return false;
    }

    public override void function()
    {
        health += healthAmaount;
        damage += amount;
        fireRate += fireRateAmount;

        level++;
    }
}
