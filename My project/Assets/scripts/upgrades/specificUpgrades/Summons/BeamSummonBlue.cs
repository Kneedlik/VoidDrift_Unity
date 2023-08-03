using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSummonBlue : upgrade
{
   [SerializeField] GameObject summon;
    GameObject pom;
    BeamSummon beam;

    float fireRate;


    void Start()
    {
        Type = type.blue;
        setColor();
    }

    public override void function()
    {
        if (level == 0)
        {
            if (SummonsManager.instance.addSummon(summon, out pom))
            {
                beam = pom.GetComponent<BeamSummon>();
                if (SummonsManager.instance.summonCount >= SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 10;
                fireRate = beam.fireRate;
                description = string.Format("Iris drone damage + 2 cooldown - 15%");
            }
        }else if(level == 1)
        {
            beam.baseDamage += 2;
            beam.fireRate -= fireRate * 0.15f;
            description = string.Format("Iris drone s beam now lasts longer dealing damage more times");

        }else if(level == 2)
        {
            beam.ticks += 2;
            beam.beamDuration += beam.tickDelay * 2;
            description = string.Format("Iris drone damage + 3 cooldown - 15%");
        }else if(level == 3)
        {
            beam.baseDamage += 3;
            beam.fireRate -= fireRate * 0.15f;
            description = string.Format("Iris drone now deals damage in a wider area");
        }else if(level == 4)
        {
            beam.Aoe = true;
            beam.ticks += 1;
            beam.beamDuration += beam.tickDelay;
        }

        level++;
    }
}
