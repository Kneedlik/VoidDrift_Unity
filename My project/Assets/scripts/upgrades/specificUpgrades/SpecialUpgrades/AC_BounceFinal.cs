using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_BounceFinal : upgrade
{
    [SerializeField] int Bounce;
    [SerializeField] int Damage;
    [SerializeField] int Speed;
    [SerializeField] int ProjectilePenalty;

    public override bool requirmentsMet()
    {
        if(levelingSystem.instance.level >= 40)
        {
            return true;
        }else return false;
    }

    

    public override void function()
    {
        AutoCannon Gun = GameObject.FindWithTag("Weapeon").GetComponent<AutoCannon>();

        Gun.baseDamage += Damage;
        Gun.Bounce += Bounce;
        Gun.ForceMultiplier += Speed;
        PlayerStats.sharedInstance.increaseProjectiles(ProjectilePenalty * -1);


    }

}
