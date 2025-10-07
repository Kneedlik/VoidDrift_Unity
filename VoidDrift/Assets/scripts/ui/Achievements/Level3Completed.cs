using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Completed : Achiavement
{
    [SerializeField] int LevelIndex;

    // Start is called before the first frame update

    public override void function(bool Win)
    {
        if (Win && SceneManager.GetActiveScene().buildIndex == LevelIndex)
        {
            Unlock();
            PrizeActivation();
            UnlockAchiavementSteam("ACH03");
        }
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.HardModeUnlocked == false)
        {
            AchiavementManager.instance.progressionState.HardModeUnlocked = true;
        }
    }
}
