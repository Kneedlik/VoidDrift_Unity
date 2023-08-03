using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "MAXHP", menuName = "upgrades/ExpIncrease")]
public class ExpirienceB : upgrade
{
    public int amount;
   

    private void Awake()
    {
        Type = type.blue;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.IncreaseEXP(amount);
    }
}
