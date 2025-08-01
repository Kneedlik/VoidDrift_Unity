using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRunes : upgradeList
{
    [SerializeField] PlayerInformation playerInformation;

    private void Start()
    {
        addUpgrades();
        ActivateAll();
        PlayerStats.sharedInstance.UpdateStats();
    }

    public void ActivateAll()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (playerInformation.EquippedRunes.Contains(list[i].upgrade.RuneId) || playerInformation.Keystone == list[i].upgrade.RuneId)
            {
                list[i].upgrade.function();
            }
        }
    }

    
}
