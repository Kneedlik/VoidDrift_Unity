using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedSwordSum : upgrade
{
    [SerializeField] GameObject summon;
    GameObject pom;

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
        if (level == 0)
        {
            if (SummonsManager.instance.addSummon(summon, out pom))
            {
                if (SummonsManager.instance.summonCount < SummonsManager.instance.maxSummons)
                {
                    cloneSelf();
                }
            }
            level++;
        }
    }
}
