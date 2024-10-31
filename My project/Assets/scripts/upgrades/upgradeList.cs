 using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] bool IncludeSpecial;

    private void Start()
    {
       addUpgrades();   
    }

    public void addUpgrades()
    {
        //Debug.Log("Starting");

        foreach(Transform asset in transform)
        {
            upgrade AssetUpgrade = asset.GetComponent<upgrade>();
            UpgradePlus upgradeTemp = new UpgradePlus();
            upgradeTemp.upgrade = AssetUpgrade;
            upgradeTemp.BaseDescription = AssetUpgrade.description;
            upgradeTemp.BaseRarity = AssetUpgrade.rarity;
            //upgradeTemp.subserviantUpgrades = AssetUpgrade.SubserviantUpgrades;
            //Debug.Log(AssetUpgrade.Type);
            if(AssetUpgrade.Type == type.special)
            {
                //Debug.Log("IsSpecial");
                foreach (Transform asset1 in transform)
                {
                    upgrade UpgradeTemp1 = asset1.GetComponent<upgrade>();
                    if (UpgradeTemp1 != null)
                    {
                        if (UpgradeTemp1.Type == type.special && UpgradeTemp1.SubserviantUpgrades.Count > 0)
                        {
                            //Debug.Log("Found");
                            for (int i = 0;i < UpgradeTemp1.SubserviantUpgrades.Count; i++)
                            {
                                if (UpgradeTemp1.SubserviantUpgrades[i].Upgrade == upgradeTemp.upgrade)
                                {
                                    //Debug.Log("Adding");
                                    upgradeTemp.SuperiorUpgrade = UpgradeTemp1;
                                    //upgradeTemp.subserviantUpgrades = UpgradeTemp1.SubserviantUpgrades;
                                    break;
                                }
                            }
                        }
                    }
                }
            }     

           list.Add(upgradeTemp);
        }

        //Adding special upgrades

        if(IncludeSpecial == false)
        {
            return;
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
                if (UpgradeTemp1.SubserviantUpgrades.Count > 0)
                {
                    for (int k = 0; k < UpgradeTemp1.SubserviantUpgrades.Count; k++)
                    {
                        if (UpgradeTemp1.SubserviantUpgrades[k].Upgrade == upgradeTemp.upgrade)
                        {
                            upgradeTemp.SuperiorUpgrade = UpgradeTemp1;
                            upgradeTemp.subserviantUpgrades = UpgradeTemp1.SubserviantUpgrades;
                            break;
                        }
                    }
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
