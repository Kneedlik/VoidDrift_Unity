using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonDamage : upgrade
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
        upgrades = GameObject.FindGameObjectsWithTag("SummonUpgrade");
        summons = new upgrade[upgrades.Length];

        for (int i = 0; i < upgrades.Length; i++)
        {
            summons[i] = upgrades[i].GetComponent<upgrade>();
            if(summons[i].level >= 3)
            {
                return true;
            }
        }

        return false;
    }

    public override void function()
    {
        PlayerStats.sharedInstance.SummonDamage += amount;

        level++;
    }
}
