 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
