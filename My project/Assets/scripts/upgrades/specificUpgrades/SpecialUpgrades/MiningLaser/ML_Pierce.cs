using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ML_Pierce : upgrade
{
    public static ML_Pierce instance;
    [SerializeField] int PierceAmount;
    [SerializeField] float AreaAmount;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        MiningLaser Laser = GameObject.FindWithTag("Weapeon").GetComponent<MiningLaser>();
        Laser.baseSize += AreaAmount;
        Laser.pierce += PierceAmount;
        level++;
    }
}
