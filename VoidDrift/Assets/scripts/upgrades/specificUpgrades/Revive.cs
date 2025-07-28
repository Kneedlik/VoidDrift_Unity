using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : upgrade
{
   [SerializeField] int amount;
    public static Revive Instance;


    void Start()
    {
        Instance = this;
        Type = type.purple;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.addRevive(amount);
        level++;
    }
}
