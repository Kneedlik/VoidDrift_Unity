using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPoison : upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        Type = type.none;
    }

    public override void function()
    {
        //Debug.Log("111");
        eventManager.OnImpact += poisonSystem.sharedInstance.BlackPoison;
        eventManager.SummonOnImpact += poisonSystem.sharedInstance.BlackPoison;
    }
}
