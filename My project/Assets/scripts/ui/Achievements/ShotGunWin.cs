using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunWin : Achiavement
{
    public override void function(bool Win)
    {
        if (Win && AchiavementManager.instance.playerInformation.WeapeonId == WeaoeonConsts.ShotGun)
        {
            Unlock();
            PrizeActivation();
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.KillSpeed) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.KillSpeed);
        }
    }
}
