using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSummon : Achiavement
{
    public override void function(bool Win)
    {
        if(Win && SummonsManager.instance.summonCount <= 0)
        {
            Unlock();
            PrizeActivation();
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.OneSummonBonus) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.OneSummonBonus);
        }
    }
}
