using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateItem
{
    public List<UpgradeItemClass> UpgradeItems;
    public string Description;
    public int Level;

    public StateItem()
    {
        UpgradeItems = new List<UpgradeItemClass>();  
        Level = levelingSystem.instance.level;
    }
}
