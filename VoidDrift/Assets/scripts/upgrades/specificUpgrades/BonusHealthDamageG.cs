using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MAXHP", menuName = "upgrades/BonusHealthDamage")]
public class BonusHealthDamageG : upgrade
{
   public static BonusHealthDamageG instance;
   public float amount;
    public int healthConversionMultiplier;
    float healthConversion;

    private void Awake()
    {
        instance = this;
        Type = type.green;
        setColor();
    }

    public override bool requirmentsMet()
    {
       if(GmaxHealthIncrease.sharedInstance.level >= 1 || BigHealth.Instance.level >= 1)
        {
            return true;
        }else return false;
    }

    public override void function()
    {
         healthConversion += amount;

        if(level == 0)
        {
            PlayerStats.OnLevel += setBonusHealthDamge;
        }
       
        level++;
    }

    public void setBonusHealthDamge()
    {
        PlayerStats.sharedInstance.damageMultiplier -= healthConversionMultiplier;

       plaerHealth health = GameObject.FindWithTag("Player").GetComponent<plaerHealth>();
        int diff = health.maxHealth - health.baseHealth;
        float pom = diff * healthConversion;
        healthConversionMultiplier = (int)pom;
        PlayerStats.sharedInstance.damageMultiplier += healthConversionMultiplier;
    }
}
