using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static Pathfinding.Util.RetainedGizmos;

public class NukeSummonGreen : upgrade
{
    [SerializeField] int DamageAmount1;
    [SerializeField] int DamageAmount2;
    [SerializeField] int DamageAmount3;
    [SerializeField] GameObject Summon;
    NukeSummon NukSum;
    GameObject pom;
    float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        Type = type.green;
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
            if (SummonsManager.instance.addSummon(Summon, out pom))
            {
                NukSum = pom.GetComponent<NukeSummon>();
                if (SummonsManager.instance.summonCount < SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;
                fireRate = NukSum.baseFireRate;
            }

            description = string.Format("Bio Sword cooldown - 30%");
        }else if (level == 1)
        {
            float pom = fireRate * 0.3f;
            NukSum.baseFireRate -= pom;
            description = string.Format("Bio Sword base Damage + {0}",DamageAmount1);
        
        }else if(level == 2)
        {
            NukSum.baseDamage += DamageAmount1;
            description = string.Format("Bio Sword base Damage + {0} Bio Sword cooldown - 20% ",DamageAmount2);
        }
        else if(level == 3)
        {
            NukSum.baseDamage += DamageAmount2;
            float pom = fireRate * 0.2f;
            NukSum.baseFireRate -= pom;
            description = string.Format("Bio Sword now deals 4% max health Damge Bio Sword base Damage + {0}",DamageAmount3);
        }
        else if(level == 4)
        {
            NukSum.baseDamage += DamageAmount3;
            NukSum.TrueDamage = 0.04f; 
        }



        level++;
    }
}
