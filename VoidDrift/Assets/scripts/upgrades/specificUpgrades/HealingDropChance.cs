using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingDropChance : upgrade
{
    [SerializeField] float Value;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.red;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.HealingOrbDropChance += Value;
        level++;
    }
}
