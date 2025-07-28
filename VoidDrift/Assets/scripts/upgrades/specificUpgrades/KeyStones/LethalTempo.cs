using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LethalTempo : upgrade
{
    // Start is called before the first frame update
    void Start()
    {
        Type = type.none;
    }

    public override void function()
    {
        eventManager.OnImpact += LethalTempoSystem.instance.Activation;
        eventManager.SummonOnImpact += LethalTempoSystem.instance.Activation;
    }
}
