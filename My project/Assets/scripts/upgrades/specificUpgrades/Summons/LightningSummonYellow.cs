using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSummonYellow : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    LightningSummon sum;

    float fireRate;


    void Start()
    {
        Type = type.yellow;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if (LightningOnHit.instance.level >= 1 && KnedlikLib.CheckSummon(this))
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
                description = string.Format("Sky beam damage + 5, CoolDown - 25%");

            }
        }else if(level == 1)
        {
            sum.baseDamage += 5;
            sum.fireRate -= fireRate * 0.25f;
            description = string.Format("Sky beam now deals damage in an area Projectiles + 1");
        }else if(level == 2)
        {
            sum.Aoe = true;
            sum.projectiles += 1;
            description = string.Format("Sky beam + 6 CoolDown - 25%");
        }else if(level == 3)
        {
            sum.damage += 6;
            sum.fireRate -= fireRate * 0.25f;
            description = string.Format("Sky beam now also spawns 5 projectiles that shock enemies, Projectiles + 1");
        }else if(level == 4)
        {
            sum.shock = true;
            sum.shockAmount = 5;
            sum.projectiles += 1;
        }

        level++;
    }

}
