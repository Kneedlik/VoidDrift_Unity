using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1CheckPoint1 : LevelCheckPoint
{
    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.DivineShield) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.DivineShield);
        }
    }
}
