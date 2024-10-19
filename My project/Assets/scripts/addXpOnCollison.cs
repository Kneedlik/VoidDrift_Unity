using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addXpOnCollison : MonoBehaviour
{
    public int xpValue = 3;
    public float PercentValue = 0;
    PlayerStats playerStats;



    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            float Multiplier = levelingSystem.instance.CalculateXpMultiplier();
            float pomDamage = PercentValue * levelingSystem.instance.xpNeeded;
            pomDamage += xpValue;
            pomDamage = pomDamage * Multiplier;
            xpValue = (int)pomDamage;

            collision.GetComponent<levelingSystem>().addXp(xpValue);
            Destroy(gameObject);
        }
    }

    public void setValue(int value)
    {
        xpValue = value;
    }

    public void setValue(int value,float percent)
    {
        xpValue = value;
        PercentValue = percent;
    }
}
