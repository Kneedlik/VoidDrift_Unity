using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioHammerGreen : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    BioHammer hammer;
    float fireRate;

    [SerializeField] int Damage1 = 10;
    [SerializeField] int Damage2 = 20;


    void Start()
    {
        Type = type.green;
        setColor();
    }

    public override bool requirmentsMet()
    {
        // if(BonusHealthDamageG.instance.level >= 3)
        // {
        //     return true;
        // }else return false;

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
                hammer = pom.GetComponent<BioHammer>();
                if (SummonsManager.instance.summonCount < SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;
                fireRate = hammer.baseFireRate;
            }

            description = string.Format("BioHammer cooldown - 30%");
        }else if(level == 1)
        {
            float pom = fireRate * 0.3f;
            hammer.baseFireRate -= pom; 

            description = string.Format("Biohammer now deals damage in an area Biohammer base damage + {0}",Damage1);
        }else if(level == 2)
        {
            hammer.Aoe = true;
            hammer.baseDamage += Damage1;

            description = string.Format("Bio Hammer base damage + {0} Bio Hammer cooldown - 20%", Damage2);
        }else if(level == 3)
        {
            float pom = fireRate * 0.2f;
            hammer.baseFireRate -= pom;
            hammer.baseDamage += Damage2;

            description = string.Format("Bio Hammer now deals 1% max Health Damage Bio hammer bonus health damage + 30%");
        }else if(level == 4)
        {
            hammer.healthScaling += 0.3f;
            hammer.TrueDamage = 0.01f;
        }

        hammer.PrintPowerLevel();
        level++;
    }
}
