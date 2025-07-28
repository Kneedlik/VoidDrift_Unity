using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronDamage : upgrade
{
    public int amount;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.iron;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.increaseDMG(amount);
        level++;
    }
}
