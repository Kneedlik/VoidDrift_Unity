using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixAchiavement : Achiavement
{
    [SerializeField] int ReviveAmount;

    public override void function(bool Win)
    {
        if (PlayerStats.sharedInstance.revives >= ReviveAmount)
        {
            Unlock();
            PrizeActivation();
        }else
        {
            int Temp = PlayerStats.sharedInstance.revives;
            if(Temp > Current)
            {
                Current = Temp;
            }
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.Phoenix) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.Phoenix);
        }
    }
}
