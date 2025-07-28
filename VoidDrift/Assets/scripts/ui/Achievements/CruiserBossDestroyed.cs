using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CruiserBossDestroyed : Achiavement
{
    public override void function(bool Win)
    {
        if(SceneManager.GetActiveScene().buildIndex == 3 && MasterManager.Instance.MapBossKill)
        {
            Unlock();
            PrizeActivation();
        }
    }

    public override void PrizeActivation()
    {
        AchiavementManager.instance.progressionState.UnlockNewWeapeon(WeaoeonConsts.Laser);
    }
}
