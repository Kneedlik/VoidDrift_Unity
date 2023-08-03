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

    public void Crit(GameObject target, int damage, ref int plusDamage)
    {

        if (critChance != 0)
        {
            int rand = Random.Range(0, 100);
            if (critChance <= rand)
            {
                float pom = damage * critMultiplier;
                plusDamage += (int)pom - damage;
                
            }
        }
    }
}
