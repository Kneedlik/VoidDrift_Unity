using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TR_Projectiles : upgrade
{
    [SerializeField] int MultishotAmount;
    public static TR_Projectiles Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        PlayerStats.sharedInstance.increaseProjectiles(MultishotAmount);
        level++;
    }
}
