using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TR_Damage : upgrade
{
    public static TR_Damage Instance;
    [SerializeField] float DamageMultiplier;
    [SerializeField] float AreaMultiplier;

    void Start()
    {
        Type = type.special;
        setColor();
        Instance = this;
    }

    public override void function()
    {
        weapeon gun = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();

        gun.damageMultiplier += DamageMultiplier;
        gun.baseSize += AreaMultiplier;

        level++;
    }

}
