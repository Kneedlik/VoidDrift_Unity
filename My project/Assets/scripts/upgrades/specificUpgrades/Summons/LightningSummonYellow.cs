using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSummonYellow : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    LightningSummon sum;

    [SerializeField] int Damage1 = 8;
    [SerializeField] int Damage2 = 12;

    float fireRate;

    void Start()
    {
        Type = type.yellow;
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

    // Update is called once per frame

    public override void function()
    {
        if(level == 0)
        {
            if (SummonsManager.instance.addSummon(summon, out pom))
            {
                sum = pom.GetComponent<LightningSummon>();
                if (SummonsManager.instance.summonCount < SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;
                fireRate = sum.baseFireRate;
                description = string.Format("Sky beam damage + {0}, CoolDown - 30%",Damage1);
            }
        }else if(level == 1)
        {
            sum.baseDamage += Damage1;
            sum.baseFireRate -= fireRate * 0.25f;
            description = string.Format("Sky beam now deals damage in an area Projectiles + 1");
        }else if(level == 2)
        {
            sum.Aoe = true;
            sum.projectiles += 1;
            description = string.Format("Sky beam damage + {0} CoolDown - 30%",Damage2);
        }else if(level == 3)
        {
            sum.baseDamage += Damage2;
            sum.baseFireRate -= fireRate * 0.25f;
            description = string.Format("Sky beam now also spawns 5 projectiles that shock enemies, Projectiles + 1");
        }else if(level == 4)
        {
            sum.shock = true;
            sum.shockAmount = 5;
            sum.projectiles += 1;
        }

        sum.PrintPowerLevel();
        level++;
    }

}
