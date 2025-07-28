using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TR_Haste : upgrade
{
    public static TR_Haste Instance;
    [SerializeField] float AsAmount;
    [SerializeField] float ProjectileSpeedAmount;
    [SerializeField] float RotationAmount;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        TracerGun gun = GameObject.FindWithTag("Weapeon").GetComponent<TracerGun>();

        gun.ForceMultiplier += ProjectileSpeedAmount;
        gun.RotationMultiplier += RotationAmount;
        gun.ASmultiplier += AsAmount;


        level++;
    }
}
