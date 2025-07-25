using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearDamage : upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        Type = type.none;
    }

    public override void function()
    {
        NearDamageSystem.instance.SetUp();
        level++;
    }
}
