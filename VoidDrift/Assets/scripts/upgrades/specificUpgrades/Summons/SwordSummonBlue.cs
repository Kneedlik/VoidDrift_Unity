using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSummonBlue : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    SwordSummon drone;
    float fireRate;

    [SerializeField] int Damage1 = 10;
    [SerializeField] int Damage2 = 8;

    
    void Start()
    {
        Type = type.blue;
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
                drone = pom.GetComponent<SwordSummon>();
                if (SummonsManager.instance.summonCount < SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;
                fireRate = drone.baseFireRate;
                description = string.Format("Drone base damage + {0}",Damage1);
            }
        }else if(level == 1)
        {
            drone.baseDamage += Damage1;
            description = string.Format("Every third slash of Shapeless is critical Drone base damage + {0}",Damage2);
        }else if(level == 2)
        {
            //float pom = fireRate * 0.2f;
            //drone.baseFireRate -= pom;
            drone.baseDamage += Damage2;
            drone.Crit = true;

            description = string.Format("Drone cooldown - 40%");
        }else if(level == 3)
        {
            float pom = fireRate * 0.4f;
            drone.baseFireRate -= pom;

            description = string.Format("Number of slashes + 1");
        }else if(level == 4)
        {
            drone.slashAmount += 1;
        }

        drone.PrintPowerLevel();
        level++;
    }

}
