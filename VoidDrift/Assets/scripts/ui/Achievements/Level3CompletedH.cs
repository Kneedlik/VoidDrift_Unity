using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3CompletedH : Achiavement
{
    public override void function(bool Win)
    {
        UnlockAchiavementSteam("ACH06");
    }

    
}
