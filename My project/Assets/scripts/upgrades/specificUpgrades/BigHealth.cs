using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHealth : upgrade
{
    public static BigHealth Instance;
    [SerializeField] float HealthAmount;

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
        float HealthTemp = health.baseHealth * HealthAmount;
        int TotalHealth = health.maxHealth + (int)HealthTemp;

        health.setMaxHP(TotalHealth);
        health.increaseHP(TotalHealth);
        level++;
    }
}
