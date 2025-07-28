using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2CompletedH : Achiavement
{
    public override void function(bool Win)
    {
        if (Win && SceneManager.GetActiveScene().buildIndex == 4)
        {
            Unlock();
            PrizeActivation();
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.CritDamage2) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.CritDamage2);
        }
    }
}
