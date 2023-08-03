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

    public override void function()
    {
        if (level == 0)
        {
            if (SummonsManager.instance.addSummon(summon, out pom))
            {
                sum = pom.GetComponent<Garlic>();
                main = sum.main;

                if (SummonsManager.instance.summonCount >= SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;

                fireRate = main.baseFireRate;
                size = main.baseSize;

                description = string.Format("Serpent base damage + 1 base area + 25%");
            }
        }
        else if (level == 1)
        {
            main.baseDamage += 1;
            float pom = size * 0.25f;
            main.baseSize += pom;

            description = string.Format("Serpent firerate + 20% base damage + 1");

        }else if(level == 2)
        {
            main.baseDamage += 1;
            float pom = fireRate * 0.2f;
            main.fireRate -= pom;

            description = string.Format("Serpent base damage + 2");


        }else if(level == 3)
        {
            main.baseDamage += 2;

            description = string.Format("Serpent base area + 30% base firerate + 25%");
        }else if(level == 4)
        {
            float pom = fireRate * 0.25f;
            main.baseFireRate -= pom;
            pom = size * 0.3f;
            main.baseSize += pom;

        }

            level++;
    }
}

