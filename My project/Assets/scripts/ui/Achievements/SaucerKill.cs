using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaucerKill : Achiavement
{
    public override void function(bool Win)
    {
        if (SceneManager.GetActiveScene().buildIndex == 5 && MasterManager.Instance.MapBossKill)
        {
            Unlock();
            PrizeActivation();
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.DroneFireRate) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.DroneFireRate);
        }
    }
}
