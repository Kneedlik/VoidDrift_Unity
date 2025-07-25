using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritUnlock : Achiavement
{
    public override void function(bool Win)
    {
        if (CritSystem.instance.critChance >= 100)
        {
            Debug.Log("Crit 100");
            Unlock();
            PrizeActivation();
        }else
        {
            int Temp = CritSystem.instance.critChance;
            if(Temp > Current)
            {
                Current = Temp;
            }
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.CritChance1) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.CritChance1);
        }

        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.CritDamage1) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.CritDamage1);
        }
    }
}
