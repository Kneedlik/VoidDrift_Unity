using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Checkpoint1H : Achiavement
{
    public float TargetTime;

    public override void function(bool Win)
    {
        if (SceneManager.GetActiveScene().buildIndex == 3 && AchiavementManager.instance.playerInformation.HardMode)
        {
            if (timer.instance.gameTime >= TargetTime)
            {
                PrizeActivation();
                Unlock();
            }
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.CritChance2) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.CritChance2);
        }
    }
}
