using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_StatStick : upgrade
{
    public static RL_StatStick instance;
    [SerializeField] float DamageAmount;
    [SerializeField] float AsAmount;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
