using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addXpOnCollison : MonoBehaviour
{
    public int xpValue = 3;
    PlayerStats playerStats;



    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            float realMultiplier = (float)playerStats.EXPmultiplier / 100f;
            float pomDamage = (float)xpValue * realMultiplier;
            xpValue = (int)pomDamage;
            collision.GetComponent<levelingSystem>().addXp(xpValue);
            Destroy(gameObject);
        }
    }

    public void setValue(int value)
    {
        xpValue = value;
    }
}
