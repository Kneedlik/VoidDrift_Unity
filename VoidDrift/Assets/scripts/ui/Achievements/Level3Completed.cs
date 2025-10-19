using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Completed : Achiavement
{
    [SerializeField] int LevelIndex;
    [SerializeField] int SlotId;

    // Start is called before the first frame update

    public override void function(bool Win)
    {
        if (Win && SceneManager.GetActiveScene().buildIndex == LevelIndex)
        {
            Unlock();
            PrizeActivation();
            UnlockAchiavementSteam("ACH03");
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.HardModeUnlocked == false)
        {
            AchiavementManager.instance.progressionState.HardModeUnlocked = true;
        }

        if (AchiavementManager.instance.progressionState.UnlockedSlots.Contains(SlotId) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedSlots.Add(SlotId);
        }
    }
}
