using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Checkpoint1 : LevelCheckPoint
{
    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.StatusDamage1) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.StatusDamage1);
        }
    }
}
