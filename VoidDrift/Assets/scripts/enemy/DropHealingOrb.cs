using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHealingOrb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DropLootOnDeath Loot = gameObject.AddComponent<DropLootOnDeath>();
        Loot.dropChancePool1.Add(PlayerStats.sharedInstance.HealingOrbDropChance);
        Loot.itemPool1.Add(PlayerStats.sharedInstance.HealingOrbPrefab);
    }

}
