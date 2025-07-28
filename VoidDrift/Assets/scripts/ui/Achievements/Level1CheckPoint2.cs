using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1CheckPoint2 : LevelCheckPoint
{
    public override void PrizeActivation()
    {
        AchiavementManager.instance.progressionState.UnlockNewWeapeon(WeaoeonConsts.ShotGun);
    }
}
