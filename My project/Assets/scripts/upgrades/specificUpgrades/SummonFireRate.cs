using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonFireRate : upgrade
{
    public float AsIncrease;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.none;
    }

    public override void function()
    {
        PlayerStats.sharedInstance.SummonCoolDown -= AsIncrease;
        level++;
    }
}
