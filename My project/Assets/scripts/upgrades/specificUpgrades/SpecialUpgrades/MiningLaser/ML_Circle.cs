using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ML_Circle : upgrade
{
    [SerializeField] float DamageMultiplier;
    [SerializeField] int ProjectileAmount;

    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override bool requirmentsMet()
    {
        if(FinalConditionsMet() && ML_FireRate.instance.level >= 1 && ML_Damage.instance.level >= 1)
        {
            return true;
        }else return false;
    }

    public override void function()
    {
        MiningLaser Laser = GameObject.FindWithTag("Weapeon").GetComponent<MiningLaser>();
        Laser.Circle = true;
        Laser.damageMultiplier += DamageMultiplier;
        PlayerStats.sharedInstance.increaseProjectiles(ProjectileAmount);
        Laser.setSideFirepoints();
        Laser.setFirepoints();
        levelingSystem.instance.FinallForm = true;
        level++;
    }
}
