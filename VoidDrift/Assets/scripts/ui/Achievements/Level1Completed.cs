using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Completed : Achiavement
{
    [SerializeField] int LevelIndex;
    [SerializeField] int SlotId;

    //private void Start()
    //{
    //    UnlockAchiavementSteam("ACH01");
    //}

    public override void function(bool Win)
    {
        if(Win && SceneManager.GetActiveScene().buildIndex == LevelIndex)
        {
            Unlock();
            PrizeActivation();
            UnlockAchiavementSteam("ACH01");
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedKeyStones.Contains(RuneConst.LethalTempo) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedKeyStones.Add(RuneConst.LethalTempo);
        }

        if (AchiavementManager.instance.progressionState.UnlockedKeyStones.Contains(RuneConst.NearDamage) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedKeyStones.Add(RuneConst.NearDamage);
        }

        AchiavementManager.instance.progressionState.UnlockNewWeapeon(WeaoeonConsts.Rockets);

        AchiavementManager.instance.progressionState.UnlockLevel(2);
        
        if (AchiavementManager.instance.progressionState.UnlockedSlots.Contains(SlotId) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedSlots.Add(SlotId);
        }
    }
}
