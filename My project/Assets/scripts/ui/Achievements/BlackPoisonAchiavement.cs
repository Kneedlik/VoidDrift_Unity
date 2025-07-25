using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPoisonAchiavement : Achiavement
{
    [SerializeField] int TargetLevel;

    public override void function(bool Win)
    {
        if(poisonOnHit.instance.level >= TargetLevel)
        {
            Unlock();
            PrizeActivation();
        }else
        {
            if(poisonOnHit.instance.level > Current)
            {
                Current = poisonOnHit.instance.level;
            }
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedKeyStones.Contains(RuneConst.NewStatus) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedKeyStones.Add(RuneConst.NewStatus);
        }
    }
}
