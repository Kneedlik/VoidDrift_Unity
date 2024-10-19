using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHealth : upgrade
{
    public static BigHealth Instance;
    [SerializeField] int HealthAmount;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Type = type.green;
        setColor();
    }

    public override void function()
    {
        GameObject player = GameObject.FindWithTag("Player");
        plaerHealth health = player.GetComponent<plaerHealth>();
        health.setMaxHP(health.maxHealth + HealthAmount);
        health.increaseHP(health.health + HealthAmount);
        level++;
    }
}
