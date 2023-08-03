using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MAXHP", menuName = "upgrades/FlatDamageG")]
public class FlatDamageG : upgrade
{
    public int amount;
     weapeon gun;

    private void Awake()
    {
        Type = type.green;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.ExtraDamage += amount;
        gun = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();
        gun.extraDamage += amount;

        level++;
    }

}
