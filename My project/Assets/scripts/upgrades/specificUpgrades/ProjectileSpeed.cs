using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpeed : upgrade
{
  public  float ForceAmount;
  public  int ASamount;

    void Start()
    {
        Type = type.yellow;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.ProjectileForce += ForceAmount;
        PlayerStats.sharedInstance.IncreaseAS(ASamount);
        level++;
    }
}
