using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MAXHP", menuName = "upgrades/DamageIncrease")]
public class RdamageIncrease : upgrade
{
    public int amount;
    public static RdamageIncrease instance;
   
    private void Awake()
    {
        instance = this;
        Type = type.red;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.increaseDMG(amount);
        level++;
    }

}
