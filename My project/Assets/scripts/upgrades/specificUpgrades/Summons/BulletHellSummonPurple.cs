using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellSummonPurple : upgrade
{
    [SerializeField] GameObject summon;
     GameObject pom;
    BulletHellSummon drone;
    float fireRate;

    void Start()
    {
        Type = type.purple;
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
                drone = pom.GetComponent<BulletHellSummon>();
                if (SummonsManager.instance.summonCount < SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;
                description = string.Format("Drone damage + 8 cooldown - 30%");
                fireRate = drone.baseFireRate;
            }
        }else if(level == 1)
        {
            float pom = fireRate * 1.3f;
            pom = pom - fireRate;
            drone.baseFireRate -= pom;

            drone.baseDamage += 8;

            description = string.Format("Drone number of bursts + 1 damage + 6");
        }else if(level == 2)
        {
            drone.bursts += 1;
            drone.baseDamage += 6;

            description = string.Format("Drone number of projectiles + 6 base procetile size + 35%");
        }else if(level == 3)
        {
            drone.projectileAmount += 6;
            drone.size += 0.35f;

            description = string.Format("Drone number of bursts + 1 pierce + 2");
        }else if(level == 4)
        {
            drone.bursts += 1;
            drone.pierce += 2;
        }


        level++;
    }
}
