using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MAXHP", menuName = "upgrades/projectileAmount")]
public class IncreasePrejectilesR : upgrade
{
    public int amount;
   

    private void Awake()
    {
        Type = type.red;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.increaseProjectiles(amount);
    }



}
