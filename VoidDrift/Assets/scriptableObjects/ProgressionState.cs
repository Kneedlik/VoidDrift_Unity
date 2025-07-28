using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Progress", menuName = "PlayerProgress")]
public class ProgressionState : ScriptableObject
{
    public int EnemiesKilled;
    public int MapEnemiesKilled;
    public int Gold;
    public List<WeapeonState> UnlockedWeapeons = new List<WeapeonState>();
    public List<int> UnlockedLevels = new List<int>();
    public List<ShopUpgradeState> ShopUpgradesProgression = new List<ShopUpgradeState>();
    public List<int> UnlockedRunes = new List<int>();
    public List<int> UnlockedKeyStones = new List<int>();
    public List<int> UnlockedSlots = new List<int>();
    public bool HardModeUnlocked;
    public List<AchiavementBasic> UnlockedAchievements = new List<AchiavementBasic>();

    public float GetTotalIncrease(int Id)
    {
        float Temp = 0;

        for (int i = 0;i < ShopUpgradesProgression.Count;i++)
        {
            if (ShopUpgradesProgression[i].Id == Id)
            {
                Temp = ShopUpgradesProgression[i].CurrentLevel * UpgradeConsts.GetIncrease(Id);
                if (Id != UpgradeConsts.Revives && Id != UpgradeConsts.Projectiles && Id != UpgradeConsts.Rerols && Id != UpgradeConsts.HealthRegen)
                {
                    Temp = Temp / 100;
                }
            }
        }

        return Temp;
    }

    public void UnlockNewWeapeon(int Id)
    {
        for (int i = 0; i < UnlockedWeapeons.Count; i++)
        {
            if (UnlockedWeapeons[i].Id == Id)
            {
                UnlockedWeapeons[i].Unlocked = true;
                UnlockedWeapeons[i].Bought = false;
                return;
            }
        }

        WeapeonState State = new WeapeonState();
        State.Id = Id;
        State.Unlocked = true;
        State.Bought = false;
    }
}
