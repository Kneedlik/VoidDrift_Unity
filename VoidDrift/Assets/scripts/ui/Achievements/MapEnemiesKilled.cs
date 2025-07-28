using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEnemiesKilled : Achiavement
{
    public int GoldReward;

    public override void function(bool Win)
    {
        if (AchiavementManager.instance.progressionState.MapEnemiesKilled >= Needed)
        {
            PrizeActivation();
            Current = Needed;
        }
        else Current = AchiavementManager.instance.progressionState.EnemiesKilled;
    }

    public override void PrizeActivation()
    {
        AchiavementManager.instance.progressionState.Gold += GoldReward;
    }
}
