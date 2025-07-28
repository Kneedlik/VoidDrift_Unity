using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmegaAchiavement : Achiavement
{
    public override void function(bool Win)
    {
        if (Omega.instance.level >= 1)
        {
            Unlock();
            PrizeActivation();
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.Omega) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.Omega);
        }
    }
}
