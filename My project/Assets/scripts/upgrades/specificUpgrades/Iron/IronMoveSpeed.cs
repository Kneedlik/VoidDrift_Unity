using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMoveSpeed : upgrade
{
    public int ASamount;
    public int SpeedAmount;

    // Start is called before the first frame update
    void Start()
    {
        Type = type.iron;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.IncreaseSpeed(SpeedAmount);
        PlayerStats.sharedInstance.IncreaseAS(ASamount);
    }
}
