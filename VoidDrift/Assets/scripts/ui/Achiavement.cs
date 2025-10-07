using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achiavement : AchievementProgress
{
    public int Needed;
    [TextArea(15, 20)]
    public string Description;
    public string SecondText;
    public Sprite Icon;
    public Color32 IconColor = new Color32(255, 255, 255, 255);
    public Sprite BackGround;
    public Sprite RewardIcon;
    public Color32 RewardIconColor = new Color32(255, 255, 255, 255);
    public int Page;
    public int Order;

    public virtual void function(bool Win)
    {

    }

    public virtual void PrizeActivation()
    {

    }

    public void UnlockAchiavementSteam(string Id)
    {
        if (Constants.Demo == false)
        {
            var Ach = new Steamworks.Data.Achievement(Id);
            Ach.Trigger();
        }
    }

    public void ClearAchiavementSteam(string Id)
    {
        var Ach = new Steamworks.Data.Achievement(Id);
        Ach.Clear();
    }

    public void Unlock()
    {
        Current = Needed;
        Unlocked = true;
        Claimed = false;
    }
    
}
