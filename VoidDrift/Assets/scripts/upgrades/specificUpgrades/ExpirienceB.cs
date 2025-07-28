using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "MAXHP", menuName = "upgrades/ExpIncrease")]
public class ExpirienceB : upgrade
{
    public int amount;
    public static ExpirienceB instance;
   

    private void Awake()
    {
        Type = type.blue;
        setColor();
        instance = this;
    }

    public override void function()
    {
        PlayerStats.sharedInstance.IncreaseEXP(amount);
    }
}
