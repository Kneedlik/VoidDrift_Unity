using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSummonGreen : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    HealingSummon heal;
    [SerializeField] float TrueHeal;


    void Start()
    {
        Type = type.green;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (KnedlikLib.CheckSummon(this))
        {
            return true;
        }
        else return false;
    }

    public override void function()
    {
        if(level == 0)
        {
            if (SummonsManager.instance.addSummon(summon, out pom))
            {
                heal = pom.GetComponent<HealingSummon>();
                if (SummonsManager.instance.summonCount < SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;
            }
            TrueHeal = heal.CalculateHealing();
            description = string.Format("Regenarete 0.3 hp every second");
           
        }else if(level == 1)
        {
            heal.regenTime = 3;
            description = string.Format("If you are at full health healing is converted into damage to random enemy");


        }else if(level == 2)
        {
            heal.transferDamage = true;
            description = string.Format("Regenarate 0.6 hp every second");
        }else if(level == 3)
        {
            heal.regenTime = 1.6f;
            description = string.Format("Regenarate 1 health every socond   damage Enemies take is multiplied by 3");
           
        }else if(level == 4)
        {
            heal.regenTime = 1;
            heal.damageMultiplier += 2;
        }

        level++;
    }
}
