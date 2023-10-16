using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioHammerGreen : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    BioHammer hammer;
    float fireRate;


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
                fireRate = hammer.fireRate;
            }

            description = string.Format("BioHammer cooldown - 30%");
        }else if(level == 1)
        {
            float pom = fireRate * 1.3f;
            pom = pom - fireRate;
            hammer.fireRate -= pom; 

            description = string.Format("Biohammer firerate + 20% Biohammer base damage + 12");
        }else if(level == 2)
        {
            float pom = fireRate * 1.2f;
            pom = pom - fireRate;
            hammer.fireRate -= pom;

            hammer.damage += 5;

            description = string.Format("Bio Hammer base damage + 20");
        }else if(level == 3)
        {
            hammer.damage += 12;

            description = string.Format("Bio Hammer now deals 1% max Health Damage Bio hammer bonus health damage + 30%");
        }else if(level == 4)
        {
            hammer.healthScaling += 0.3f;
        }

        level++;
    }
}
