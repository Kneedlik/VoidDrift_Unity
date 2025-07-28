using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : upgrade
{
    [SerializeField] float RegenAmount;

    // Start is called before the first frame update
    void Start()
    {
        Type = type.green;
        setColor();
    }

    public override void function()
    {
        plaerHealth Health = GameObject.FindWithTag("Player").GetComponent<plaerHealth>();
        Health.Regen += RegenAmount;
        level++;
    }
}
