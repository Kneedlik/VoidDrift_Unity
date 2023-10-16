using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSummonBlue : upgrade
{
   [SerializeField] GameObject summon;
    GameObject pom;
    SwordSummon drone;
    float fireRate;

    
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
                description = string.Format("Drone base damage + 4");
            }
        }else if(level == 1)
        {
            drone.baseDamage += 4;
            description = string.Format("Every fourth slash of Shapeless is critical");
        }else if(level == 2)
        {
           // float pom = fireRate * 0.2f;
           // drone.baseFireRate -= pom;

            drone.Crit = true;

            description = string.Format("Drone cooldown - 25% damage + 4");
        }else if(level == 3)
        {
            float pom = fireRate * 0.15f;
            drone.baseFireRate -= pom;

            description = string.Format("Number of slashes + 1");
        }else if(level == 4)
        {
            drone.slashAmount += 1;
        }


        level++;
    }

}
