using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AchievementProgress : MonoBehaviour
{
    public int Id;
    public bool Unlocked;
    public bool Claimed;
    public int Current;

    public void UpdateProgress()
    {
        for (int i = 0; i < AchiavementManager.instance.progressionState.UnlockedAchievements.Count; i++)
        {
            if (AchiavementManager.instance.progressionState.UnlockedAchievements[i] != null)
            {
                if (AchiavementManager.instance.progressionState.UnlockedAchievements[i].Id == Id)
                {
                    AchiavementManager.instance.progressionState.UnlockedAchievements[i].Unlocked = Unlocked;
                    AchiavementManager.instance.progressionState.UnlockedAchievements[i].Claimed = Claimed;
                    AchiavementManager.instance.progressionState.UnlockedAchievements[i].Current = Current;
                    return;
                }
            }
        }
        AchiavementBasic Temp = new AchiavementBasic();
        Temp.Id = Id;
        Temp.Unlocked = Unlocked;
        Temp.Claimed = Claimed ;
        Temp.Current = Current;

        AchiavementManager.instance.progressionState.UnlockedAchievements.Add(Temp);
    }

    public void LoadProgress()
    {
        for (int i = 0; i < AchiavementManager.instance.progressionState.UnlockedAchievements.Count; i++)
        {
            if (AchiavementManager.instance.progressionState.UnlockedAchievements[i] != null)
            {
                if (AchiavementManager.instance.progressionState.UnlockedAchievements[i].Id == Id)
                {
                    Unlocked = AchiavementManager.instance.progressionState.UnlockedAchievements[i].Unlocked;
                    Claimed = AchiavementManager.instance.progressionState.UnlockedAchievements[i].Claimed;
                    Current = AchiavementManager.instance.progressionState.UnlockedAchievements[i].Current;
                    return;
                }
            }
        }
    }
}
