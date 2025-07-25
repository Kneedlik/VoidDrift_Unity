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
        if (RuneId == 0)
        {
            instance = this;
            Type = type.purple;
            setColor();
        }else
        {
            Type = type.none;
        }
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
        if (RuneId == 0)
        {
            health += healthAmaount;
            damage += amount;
            fireRate += fireRateAmount;
        }else
        {
            statsOnRevive.instance.health += healthAmaount;
            statsOnRevive.instance.damage += amount;
            statsOnRevive.instance.fireRate += fireRateAmount;
        }

        level++;
    }
}
