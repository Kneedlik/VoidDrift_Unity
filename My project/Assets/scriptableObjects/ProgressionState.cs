using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Progress", menuName = "PlayerProgress")]
public class ProgressionState : ScriptableObject
{
    public int Gold;
    public List<int> UnlockedWeapeons = new List<int>();
    public List<int> UnlockedLevels = new List<int>();
    public Dictionary<int, ShopUpgradeState> ShopUpgradesProgression  = new Dictionary<int, ShopUpgradeState>();

    public float GetTotalIncrease(int Id)
    {
        float Temp;

        if(ShopUpgradesProgression.ContainsKey(Id))
        {
            Temp = ShopUpgradesProgression[Id].CurrentLevel * UpgradeConsts.GetIncrease(Id);

        }else Temp = 0;

        return Temp;
    }
}
