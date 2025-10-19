using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3CompletedH : Achiavement
{
    public override void function(bool Win)
    {
        if (Win && SceneManager.GetActiveScene().buildIndex == 5 && MasterManager.Instance.PlayerInformation.HardMode)
        {
            UnlockAchiavementSteam("ACH06");
        }
    }

    
}
