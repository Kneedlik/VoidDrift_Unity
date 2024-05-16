 using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;


[System.Serializable]
public class UpgradePlus
{
    public upgrade upgrade;
    public string BaseDescription;
    public int BaseRarity;
    public int NotChoosen;
    public int NotApeared;
    public int ApearedInRow;
    public int TrueRarity;

    public upgrade SuperiorUpgrade;
    public List<SubserviantUpgrade> subserviantUpgrades = new List<SubserviantUpgrade>();

    public UpgradePlus()
    {
        NotChoosen = 0;
        NotApeared = 0;
        ApearedInRow = 0;
        TrueRarity = 0;
    }
}

public class upgradeList : MonoBehaviour
{  
    public List<UpgradePlus> list = new List<UpgradePlus>();

    private void Start()
    {
       addUpgrades();   
    }

    public void addUpgrades()
    {
        foreach(Transform asset in transform)
        {
            UpgradePlus upgradeTemp = new UpgradePlus();
            upgradeTemp.upgrade = asset.GetComponent<upgrade>();
            upgradeTemp.BaseDescription = asset.GetComponent<upgrade>().description;
            upgradeTemp.BaseRarity = asset.GetComponent<upgrade>().rarity;
           /* if(upgradeTemp.upgrade.Type == type.special)
            {
                foreach (Transform asset1 in transform)
                {
                    upgrade UpgradeTemp1 = asset1.GetComponent<upgrade>();
                    if (UpgradeTemp1 != null)
                    {
                        if (UpgradeTemp1.Type == type.special && UpgradeTemp1.SubserviantUpgrades != null)
                        {
                            for (int i = 0;i < UpgradeTemp1.SubserviantUpgrades.Count; i++)
                            {
                                if (UpgradeTemp1.SubserviantUpgrades[i].Upgrade == upgradeTemp.upgrade)
                                {
                                    upgradeTemp.SuperiorUpgrade = UpgradeTemp1;
                                    upgradeTemp.subserviantUpgrades = UpgradeTemp1.SubserviantUpgrades;
                                    break;
                                }
                            }
                        }
                    }
                }
            }     */

           list.Add(upgradeTemp);
        }

        weapeon Weapeon = GameObject.FindWithTag("Weapeon").GetComponent<weapeon>();
        for (int i = 0;i < Weapeon.WeapeonUpgrades.Count;i++)
        {
            UpgradePlus upgradeTemp = new UpgradePlus();
            upgradeTemp.upgrade = Weapeon.WeapeonUpgrades[i];
            upgradeTemp.BaseDescription = Weapeon.WeapeonUpgrades[i].description;
            upgradeTemp.BaseRarity = Weapeon.WeapeonUpgrades[i].rarity;
            for (int j = 0; j < Weapeon.WeapeonUpgrades.Count; j++)
            {
                upgrade UpgradeTemp1 = Weapeon.WeapeonUpgrades[j];
                if (UpgradeTemp1.SubserviantUpgrades != null)
                {
                    upgradeTemp.SuperiorUpgrade = UpgradeTemp1;
                    upgradeTemp.subserviantUpgrades = UpgradeTemp1.SubserviantUpgrades;
                    break;
                }
            }

            list.Add(upgradeTemp);
        }
    }

    public void addNewUpgrade(upgrade e)
    {
        UpgradePlus upgradeTemp = new UpgradePlus();
        upgradeTemp.upgrade = e;
        upgradeTemp.BaseDescription = e.description;
        upgradeTemp.BaseRarity = e.rarity;
        list.Add(upgradeTemp);
    }

    public int CountLevels()
    {
        int amount = 0;

        for (int i = 0; i < list.Count; i++)
        {
            amount += list[i].upgrade.level;
        }

        Debug.Log(amount);
        return amount;
    }

    public int CountColor(type selectedColor)
    {
        int amount = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].upgrade.Type == selectedColor)
            {
                amount += list[i].upgrade.level;
            }
        }
        Debug.Log(amount);
        return amount;
    }

}
