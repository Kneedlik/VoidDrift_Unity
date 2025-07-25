using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Checkpoint1 : LevelCheckPoint
{
    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.StatusTick) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.StatusTick);
        }
    }
}
