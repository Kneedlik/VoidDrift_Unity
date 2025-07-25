using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransporterKilled : Achiavement
{
    public override void function(bool Win)
    {
        if(SceneManager.GetActiveScene().buildIndex == 3 && MasterManager.Instance.MapBoss2Kill)
        {
            Unlock();
            PrizeActivation();
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.HealthDamage) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.HealthDamage);
        }
    }
}
