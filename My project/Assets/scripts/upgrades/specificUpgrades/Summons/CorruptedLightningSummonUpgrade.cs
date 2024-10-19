using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class CorruptedLightningSummonUpgrade : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;
    LightningSummon sum;

    void Start()
    {
        Type = type.currupted;
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
        if (SummonsManager.instance.addSummon(summon, out pom))
        {
            sum = pom.GetComponent<LightningSummon>();
            if (SummonsManager.instance.summonCount < SummonsManager.instance.maxSummons)
            {
                cloneSelf();
            }  
            sum.PrintPowerLevel();
        }

        level++;
    }
}
