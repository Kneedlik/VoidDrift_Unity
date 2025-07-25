using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1CompletedHard : Achiavement
{
    public override void function(bool Win)
    {
        if (Win && SceneManager.GetActiveScene().buildIndex == 3)
        {
            Unlock();
            PrizeActivation();
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.StatusDamage2) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.StatusDamage2);
        }
    }
}
