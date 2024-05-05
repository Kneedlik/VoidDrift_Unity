using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMarkerSummonRed : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    TargetMarker marker;
    float fireRate;


    void Start()
    {
        Type = type.red;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if(PrimeOnHit.instance.level >= 1 && KnedlikLib.CheckSummon(this))
        {
            return true;
        }else return false;  
    }

    public override void function()
    {
        if(level == 0)
        {
            if (SummonsManager.instance.addSummon(summon, out pom))
            {
                marker = pom.GetComponent<TargetMarker>();
                if(SummonsManager.instance.summonCount < SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
                rarity -= 5;
                fireRate = marker.baseFireRate;

                description = string.Format("Spoter Drone cooldown - 25%");

            }
        }else if(level == 1)
        {
            float pom = fireRate * 1.25f;
            pom = pom - fireRate;
            marker.baseFireRate -= pom;

            description = string.Format("Spoter drone targets + 1");
        }
        else if(level == 2)
        {
            marker.targets += 1;
            description = string.Format("Spoter drone cooldown - 30%");

        }else if(level == 3)
        {
            float pom = fireRate * 1.3f;
            pom = pom - fireRate;
            marker.baseFireRate -= pom;

            description = string.Format("Spoter drone targets + 1");
        }else if(level == 4)
        {
            marker.targets += 1;
        }

        level++;
    }
}
