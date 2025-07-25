using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl40 : Achiavement
{
    public override void function(bool Win)
    {
        if(levelingSystem.instance.level >= 40)
        {
            Unlock();
            PrizeActivation();
        }else
        {
            int Temp = levelingSystem.instance.level;
            if(Temp > Current)
            {
                Current = Temp;
            }
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.ExtraDamage) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.ExtraDamage);
        }
    }
}
