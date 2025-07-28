using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDamage : upgrade
{
    [SerializeField] float DamageAmount;

    // Start is called before the first frame update
    void Start()
    {
        Type = type.none;
    }

    public override void function()
    {
        PlayerStats.sharedInstance.StatusDamage += DamageAmount;
        level++;
    }
}
