using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicYellow : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    GarlicMain main;
    Garlic sum;

    float fireRate;
    float size;
   
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

    public override void function()
    {
        if (level == 0)
        {
            if (SummonsManager.instance.addSummon(summon, out pom))
            {
                sum = pom.GetComponent<Garlic>();
                main = sum.main;

                if (SummonsManager.instance.summonCount < SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;

                fireRate = main.baseFireRate;
                size = main.baseSize;

                description = string.Format("Serpent base damage + 3 area + 30%");
            }
        }
        else if (level == 1)
        {
            main.baseDamage += 3;
            float pom = size * 0.30f;
            main.baseSize += pom;

            description = string.Format("Serpent firerate + 25% damage + 2");

        }else if(level == 2)
        {
            main.baseDamage += 2;
            float pom = fireRate * 0.25f;
            main.fireRate -= pom;

            description = string.Format("Serpent base damage + 6");


        }else if(level == 3)
        {
            main.baseDamage += 6;

            description = string.Format("Serpent area + 40% cooldown - 30%");
        }else if(level == 4)
        {
            float pom = fireRate * 0.3f;
            main.baseFireRate -= pom;
            pom = size * 0.4f;
            main.baseSize += pom;

        }
            level++;
    }
}

