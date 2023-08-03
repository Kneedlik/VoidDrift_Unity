using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSummonPurple : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    SpinningSummonMain main;
    SpiningSummon sum;

    float size;
    float distance;
    float fireRate;

    void Start()
    {
        Type = type.purple;
        setColor();
    }


    public override void function()
    {
        if (level == 0)
        {
            if (SummonsManager.instance.addSummon(summon, out pom))
            {
                sum = pom.GetComponent<SpiningSummon>();
                main = sum.main.GetComponent<SpinningSummonMain>();

                if (SummonsManager.instance.summonCount >= SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;

                 size = main.baseSize;
                 distance = main.orbDistance;
                fireRate = main.fireRate;

                description = string.Format("Orb base damage + 6 firerate + 10% ");
                 
            }        

        }

        else if (level == 1)
        {
            main.baseDamage += 5;
            float pom = fireRate * 0.1f;
            main.baseFireRate += pom;

            description = string.Format("Drone projectiles + 2 orbs distance + 35%");
        }else if (level == 2)
        {
            main.increaseOrbCount(2);
            float pom = distance * 0.35f;
            main.orbDistance += pom;
            main.setDistance();

            description = string.Format("Drone base damage + 8 Base size + 25%");
        }
        else if(level == 3)
        {
            main.baseDamage += 8;
            float pom = size * 0.25f;
            main.baseSize += pom;

            description = string.Format("Drone projectiles + 1 orb base size + 25% orb base distance + 25");
        }else if(level == 4)
        {
            main.increaseOrbCount(1);
            float pom = distance * 0.25f;
            main.orbDistance += pom;
            main.setDistance();

            pom = size * 0.25f;
            main.baseSize += pom;
        }

            level++;
    }
}
