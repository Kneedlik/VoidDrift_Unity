using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisDrones : Achiavement
{

    public override void function(bool Win)
    {
        Summon[] summons = FindObjectsOfType<Summon>();
        int Count = 0;
        for (int i = 0; i < summons.Length; i++)
        {
            if (summons[i].id == 9)
            {
                Count = Count + 1;
            }
        }

        if(Count >= 5)
        {
            Unlock();
            PrizeActivation();
        }else
        {
            if(Count > Current)
            {
                Current = Count;
            }
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedKeyStones.Contains(RuneConst.Lasers) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedKeyStones.Add(RuneConst.Lasers);
        }
    }
}
