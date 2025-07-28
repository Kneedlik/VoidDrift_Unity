using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonDamagePlus : upgrade
{
    [SerializeField] int amount;
    GameObject[] upgrades;
    upgrade[] summons;


    void Start()
    {
        Type = type.purple;
        setColor();
    }

    public override bool requirmentsMet()
    {
        int level = 0;
        upgrades = GameObject.FindGameObjectsWithTag("SummonUpgrade");
        summons = new upgrade[upgrades.Length];

        for (int i = 0; i < upgrades.Length; i++)
        {
            summons[i] = upgrades[i].GetComponent<upgrade>();
            level += summons[i].level;
        }

        if (level >= 25)
        {
            return true;
        }
        else return false;
        
    }

    public override void function()
    {
        PlayerStats.sharedInstance.SummonDamage += amount;

        level++;
    }
}
