using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachneGunSummonRed : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    MachneGun drone;

    float fireRate;

    

    void Start()
    {
        Type = type.red;
        setColor();
    }

    public override void function()
    {
        if(level == 0)
        {
           if(SummonsManager.instance.addSummon(summon,out pom))
           {
                drone = pom.GetComponent<MachneGun>();
                if(SummonsManager.instance.summonCount >= SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;
                fireRate = drone.baseFireRate;
                description = string.Format("Drone base damage + 3");

            }
        }else
        if(level == 1)
        {
           
            drone.baseDamage += 3;
            description = string.Format("Drone projectiles + 1");
            
        }else if(level == 2)
        {
            drone.bulletsInBurst += 1;

            description = string.Format("Drone firerate + 20% ");

        }else if(level == 3)
        {
            float pom = fireRate * 1.2f;
            pom = pom - fireRate;
            drone.baseFireRate -= pom;

            description = string.Format("Drone projectiles +1 base damage + 3");
        }else if(level == 4)
        {
            drone.bulletsInBurst += 1;
            drone.baseDamage += 3;
        }


        level++;
       
    }

}
