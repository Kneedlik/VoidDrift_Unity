using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TR_PierceFinal : upgrade
{
    [SerializeField] int PierceAmount;
    [SerializeField] float SpeedMultiplier;
    [SerializeField] float RotationMultiplier;
    [SerializeField] float DamageMultiplier;
    [SerializeField] float ASPenalty;

    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override bool requirmentsMet()
    {
        //if (FinalConditionsMet() && TR_Pierce.instance.level >= 1 && TR_Damage.Instance.level >= 1)
        //{
            return true;
        //}
        //return false;
    }

    public override void function()
    {
        TracerGun gun = GameObject.FindWithTag("Weapeon").GetComponent<TracerGun>();

        if (gun != null)
        {
            gun.ForceMultiplier += SpeedMultiplier;
            gun.RotationMultiplier += RotationMultiplier;
            gun.damageMultiplier += DamageMultiplier;
            gun.pierce += PierceAmount;
            gun.ASmultiplier -= ASPenalty;
            gun.PierceFinal = true;
            gun.setFirepoints();
            gun.setSideFirepoints();
        }
    }
}
