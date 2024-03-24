using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossDeath : DeathFunc
{
    [SerializeField] float VictoryDelay;

    private void Start()
    {
        Health health = GetComponent<Health>();
        health.DeathFunc.Add(this);
       
    }

    public override void function()
    {
        timer.instance.VictoryDelayed(VictoryDelay);
    }

   
}
