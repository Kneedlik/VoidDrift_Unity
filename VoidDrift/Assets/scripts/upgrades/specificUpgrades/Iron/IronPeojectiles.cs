using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronPeojectiles : upgrade
{
    [SerializeField] int amount;

    void Start()
    {
        Type = type.iron;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.increaseProjectiles(amount);
    }
}
