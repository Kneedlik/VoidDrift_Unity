using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TR_Summon : upgrade
{
    // Start is called before the first frame update
    public static TR_Summon instance;
    [SerializeField] int Amount;


    void Start()
    {
        instance = this;
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        TracerGun TR = GameObject.FindWithTag("Weapeon").GetComponent<TracerGun>();
        TR.SummonCount += Amount;

        if(level == 0)
        {
            description = string.Format("Drones + 2");
        }

        level++;
    }
}
