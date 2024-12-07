using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_Area : upgrade
{
    public static RL_Area Instance;
    [SerializeField] float AreaMultiplier;
    [SerializeField] int ProjectileAmount;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        rocketLauncher RocketLauncher = GameObject.FindWithTag("Weapeon").GetComponent<rocketLauncher>();
        PlayerStats.sharedInstance.increaseProjectiles(ProjectileAmount);
        RocketLauncher.baseSize += AreaMultiplier;
        level++;
    }
}
