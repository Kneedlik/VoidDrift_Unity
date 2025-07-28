using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachneGunSummonRed : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    MachneGun drone;

    [SerializeField] int Damage1 = 8;
    [SerializeField] int Damage2 = 6;
    [SerializeField] int Damage3 = 12;

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
                description = string.Format("Drone base damage + {0}",Damage1);
            }
        }else
        if(level == 1)
        {
            drone.baseDamage += Damage1;
            description = string.Format("Drone projectiles + 4 projectile speed + 40%");
            
        }else if(level == 2)
        {
            drone.bulletsInBurst += 4;
            drone.BaseForce = drone.BaseForce + (drone.BaseForce * 0.4f);
            description = string.Format("Drone cooldown - 40% base damage + {0}",Damage2);

        }else if(level == 3)
        {
            float pom = drone.baseFireRate * 0.4f;
            drone.baseFireRate -= pom;
            drone.baseDamage += Damage2;

            description = string.Format("Drone Pierce + 1 base damage + {0}",Damage3);
        }else if(level == 4)
        {
            drone.Pierce += 1;
            drone.baseDamage += Damage3;
        }

        drone.PrintPowerLevel();
        level++;
       
    }

}
