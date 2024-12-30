using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ML_Inferno : upgrade
{
    //[SerializeField] DrillSystem drillSystem;
    [SerializeField] LaserInfernoBurnSystem StatusSystem;
    [SerializeField] float AreaAmount;

    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if(FinalConditionsMet() && ML_Pierce.instance.level >= 1 && ML_Damage.instance.level >= 1)
        {
            return true;
        }return false;
    }

    public override void function()
    {
        MiningLaser Laser = GameObject.FindWithTag("Weapeon").GetComponent<MiningLaser>();
        StatusSystem.enabled = true;
        eventManager.ImpactGunOnlyHitScan += StatusSystem.Burn;
        //drillSystem.TrueDamage = 0.0005f;
        Laser.pierce += 99;
        Laser.baseSize += AreaAmount;
        Laser.Inferno = true;
        Laser.setFirepoints();
        Laser.setSideFirepoints();
        levelingSystem.instance.FinallForm = true;
        level++;
    }
}
