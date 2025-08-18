using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementsUiManager : AchiavementManager
{
    public List<AchiavementBox> Boxes = new List<AchiavementBox>();
    public int CurrentPage;

    private void Awake()
    {
        ProgressionState TempP = SaveManager.LoadPlayerProgress();
        if (TempP != null)
        {
            progressionState = TempP;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Prepare();
        LoadPage(CurrentPage);
    }

    public void ExitToPlayerMenu()
    {
        for (int i = 0; i < achiavements.Count; i++) 
        {
            if (achiavements[i].Claimed == false && achiavements[i].Unlocked)
            {
                achiavements[i].Claimed = true;
            }
        }

        SaveAllToProgress();
        SaveManager.SavePlayerProgress(progressionState);

        SceneManager.LoadScene(1);
    }

    public void LoadPage(int Page)
    {
        int j = 0;

        for (int i = 0; i < achiavements.Count; i++)
        {
            if (achiavements[i].Page == Page)
            {
                Boxes[j].LoadAchievement(achiavements[i]);
                j++;
            }
        }

        if(j < 8)
        {
            while(j < 8)
            {
                Boxes[j].UnloadAchiavement();
                j++;
            }
        }
    }

    public void LoadNextPage()
    {
        CurrentPage = CurrentPage + 1; 
        if(CurrentPage > MaxPage)
        {
            CurrentPage = MaxPage;
        }
        LoadPage(CurrentPage);
    }

    public void LoadPreviousPage()
    {
        CurrentPage = CurrentPage - 1;
        if(CurrentPage < 1)
        {
            CurrentPage = 1;
        }
        LoadPage(CurrentPage);
    }
}
