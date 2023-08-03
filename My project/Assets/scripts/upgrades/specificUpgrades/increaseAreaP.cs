using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MAXHP", menuName = "upgrades/AreaIncrease")]
public class increaseAreaP : upgrade
{
    public static increaseAreaP sharedInstance;
    public int area;
    public int damage;
   

    private void Awake()
    {
        sharedInstance = this;
        Type = type.purple;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.increaseAREA(area);
        PlayerStats.sharedInstance.increaseDMG(damage);
        level++;
       
    }
}
