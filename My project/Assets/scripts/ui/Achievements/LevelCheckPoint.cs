using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCheckPoint : Achiavement
{
    public float TargetTime;
    public int LevelIndex;

    public override void function(bool Win)
    {
        if (SceneManager.GetActiveScene().buildIndex == LevelIndex)
        {
            if (timer.instance.gameTime >= TargetTime)
            {
                PrizeActivation();
                Unlock();
            }
        }
    }
}
