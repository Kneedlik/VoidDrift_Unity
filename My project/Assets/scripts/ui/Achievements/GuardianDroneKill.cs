using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardianDroneKill : Achiavement
{
    public override void function(bool Win)
    {
        if (SceneManager.GetActiveScene().buildIndex == 4 && MasterManager.Instance.MapBossKill)
        {
            Unlock();
            PrizeActivation();
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.SummonAndDamage) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.SummonAndDamage);
        }
    }
}
