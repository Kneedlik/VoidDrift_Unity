using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        Type = type.none;
    }

    public override void function()
    {
        LasersSystem.instance.enabled = true;
    }
}
