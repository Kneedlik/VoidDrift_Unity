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


    public override void function()
    {
        if (level == 0)
        {
            if (SummonsManager.instance.addSummon(summon, out pom))
            {
                drone = pom.GetComponent<BulletHellSummon>();
                if (SummonsManager.instance.summonCount >= SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;
                description = string.Format("Drone base damage + 3 firerate + 20%");
                fireRate = drone.baseFireRate;
            }
        }else if(level == 1)
        {
            float pom = fireRate * 1.25f;
            pom = pom - fireRate;
            drone.baseFireRate -= pom;

            drone.baseDamage += 3;

            description = string.Format("Drone number of bursts + 1 damage + 3");
        }else if(level == 2)
        {
            drone.bursts += 1;
            drone.baseDamage += 3;

            description = string.Format("Drone number of projectiles + 4 base procetile size + 25%");
        }else if(level == 3)
        {
            drone.projectileAmount += 4;
            drone.size += 0.25f;

            description = string.Format("Drone number of bursts + 1 pierce + 2");
        }else if(level == 4)
        {
            drone.bursts += 1;
            drone.pierce += 2;
        }


        level++;
    }
}
