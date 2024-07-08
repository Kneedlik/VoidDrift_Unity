using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_StatStick : upgrade
{
    [SerializeField] float DamageAmount;
    [SerializeField] float AsAmount;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        rocketLauncher RocketLauncher = GameObject.FindWithTag("Weapeon").GetComponent<rocketLauncher>();
        RocketLauncher.damageMultiplier += DamageAmount;
        RocketLauncher.ASmultiplier += AsAmount;
        level++;
    }
}
