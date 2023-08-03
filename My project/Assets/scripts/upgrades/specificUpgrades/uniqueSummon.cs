using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uniqueSummon : upgrade
{
    int uniqueSummons = 0;
    public int Increase;
    public int amount;


    void Start()
    {
        Type = type.blue;
        setColor();
    }

    public override bool requirmentsMet()
    {
       if(SummonsManager.instance.summonCount >= 3)
        {
            return true;
        }else return false;
    }

    public override void function()
    {
        Increase += amount;

        if(level == 0)
        {
            PlayerStats.OnLevel += UniqueSummonDammage;
        }

        level++;
    }

    public void UniqueSummonDammage()
    {
        Summon[] summons = FindObjectsOfType<Summon>();
        List<int> ids = new List<int>();
        int pom = 0;

        for (int i = 0; i < summons.Length; i++)
        {
            if(ids.Contains(summons[i].id) != true)
            {
                ids.Add(summons[i].id);
                pom++;
            }
        }

        int diff = pom - uniqueSummons;

        for (int i = 0; i < diff; i++)
        {
            PlayerStats.sharedInstance.damageMultiplier += Increase;
        }

        uniqueSummons = pom;

    }

}
