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
        if(level == 0)
        {
           if(SummonsManager.instance.addSummon(summon,out pom))
           {
                drone = pom.GetComponent<MachneGun>();
                if(SummonsManager.instance.summonCount < SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;
                fireRate = drone.baseFireRate;
                description = string.Format("Drone base damage + 4");
            }
        }else
        if(level == 1)
        {
            drone.baseDamage += 4;
            description = string.Format("Drone projectiles + 1 projectile speed + 30%");
            
        }else if(level == 2)
        {
            drone.bulletsInBurst += 1;

            description = string.Format("Drone cooldown - 40% base damage + 3");

        }else if(level == 3)
        {
            float pom = fireRate * 1.4f;
            pom = pom - fireRate;
            drone.baseFireRate -= pom;
            drone.baseDamage += 3;

            description = string.Format("Drone projectiles +1 base damage + 6");
        }else if(level == 4)
        {
            drone.bulletsInBurst += 1;
            drone.baseDamage += 6;
        }


        level++;
       
    }

}
