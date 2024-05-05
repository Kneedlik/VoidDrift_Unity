using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritSystem : MonoBehaviour
{
    public static CritSystem instance;
    public int critChance;
    public float critMultiplier = 2;

    private void Awake()
    {
        instance = this;
    }

    public Color32 Crit(GameObject target, int damage, ref int plusDamage)
    {
        if (critChance != 0)
        {
            int rand = Random.Range(0, 100);
            Debug.Log(critChance);
            Debug.Log(rand);
            if (rand <= critChance)
            {
                float pom = damage * critMultiplier;
                plusDamage += (int)pom - damage;
                return new Color32(255, 0, 0, 255);   
            }
        }
        return new Color32(0, 0, 0, 0);
    }
}
