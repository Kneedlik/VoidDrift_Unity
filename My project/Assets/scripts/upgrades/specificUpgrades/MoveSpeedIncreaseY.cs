using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "MAXHP", menuName = "upgrades/MoveSpeedIncrease")]
public class MoveSpeedIncreaseY : upgrade
{
    public static MoveSpeedIncreaseY instance;
    public int ASamount;
    public int SpeedAmount;
    
    private void Awake()
    {
        instance = this;
        Type = type.yellow;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.IncreaseSpeed(SpeedAmount);
        PlayerStats.sharedInstance.IncreaseAS(ASamount);
    }
}
