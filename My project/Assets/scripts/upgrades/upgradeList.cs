 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeList : MonoBehaviour
{
    public List<upgrade> list = new List<upgrade>();

    private void Start()
    {
       addUpgrades();   
    }

    public void addUpgrades()
    {
        foreach(Transform asset in transform)
        {
            list.Add(asset.GetComponent<upgrade>());
        }
    }

    public void addNewUpgrade(upgrade e)
    {
        list.Add(e);
    }

    public int CountLevels()
    {
        int amount = 0;

        for (int i = 0; i < list.Count; i++)
        {
            amount += list[i].level;
        }

        Debug.Log(amount);
        return amount;
    }

    public int CountColor(type selectedColor)
    {
        int amount = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Type == selectedColor)
            {
                amount += list[i].level;
            }
        }
        Debug.Log(amount);
        return amount;
    }
}
