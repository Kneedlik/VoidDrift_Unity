using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSummonBlue : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    BeamSummon beam;

    [SerializeField] int Damage1 = 6;
    [SerializeField] int Damage2 = 8;

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
                beam = pom.GetComponent<BeamSummon>();
                if (SummonsManager.instance.summonCount < SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;
                fireRate = beam.baseFireRate;
                description = string.Format("Iris drone damage + {0} cooldown - 20%",Damage1);
            }
        }else if(level == 1)
        {
            beam.baseDamage += Damage1;
            beam.baseFireRate -= fireRate * 0.2f;
            description = string.Format("Iris drone s beam now lasts longer dealing damage more times");

        }else if(level == 2)
        {
            beam.ticks += 2;
            beam.beamDuration += beam.tickDelay * 2;
            description = string.Format("Iris drone damage + {0} cooldown - 25%",Damage2);
        }else if(level == 3)
        {
            beam.baseDamage += Damage2;
            beam.baseFireRate -= fireRate * 0.25f;
            description = string.Format("Iris drone now deals damage in a wider area, Ticks + 1");
        }else if(level == 4)
        {
            beam.Aoe = true;
            beam.ticks += 1;
            beam.beamDuration += beam.tickDelay;
        }

        beam.PrintPowerLevel();
        level++;
    }
}
