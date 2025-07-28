using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Completed : Achiavement
{
    [SerializeField] int LevelIndex;
    [SerializeField] int SlotId;

    public override void function(bool Win)
    {
        if (Win && SceneManager.GetActiveScene().buildIndex == LevelIndex)
        {
            Unlock();
            PrizeActivation();
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedKeyStones.Contains(RuneConst.BlackHoles) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedKeyStones.Add(RuneConst.BlackHoles);
        }

        AchiavementManager.instance.progressionState.UnlockNewWeapeon(WeaoeonConsts.Tracer);

        if(AchiavementManager.instance.progressionState.UnlockedSlots.Contains(SlotId) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedSlots.Add(SlotId);
        }
    }
}
