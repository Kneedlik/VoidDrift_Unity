using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiavementManager : MonoBehaviour
{
    public static AchiavementManager instance;
    public ProgressionState progressionState;
    public  PlayerInformation playerInformation;
    public List<Achiavement> achiavements = new List<Achiavement>();
    public int MaxPage;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Prepare();
    }

    public void Prepare()
    {
        LoadIntoList();
        LoadAllAchiavements();
        SortPages();
    }

    public void LoadAllAchiavements()
    {
        Debug.Log(achiavements.Count);
        for (int i = 0; i < achiavements.Count; i++)
        {
            achiavements[i].LoadProgress(); 
        }
    }

    public void LoadIntoList()
    {
        foreach (Transform asset in transform)
        {
            Achiavement TempAc = asset.GetComponent<Achiavement>();
            if (TempAc != null) 
            {
                achiavements.Add(TempAc);
            }
        }

        //Debug.Log(achiavements.Count);
    }

    public void SortPages()
    {
        int MaxOnPage = 8;
        int Page = 1;
        int Index = 1;
        int LowestIndex = 4;
        bool Found = false;

        List<Achiavement> TempList = new List<Achiavement>();

        for (int i = 0; i < achiavements.Count; i++)
        {
            for (int j = 0; j < achiavements.Count;j++)
            {
                if (achiavements[j].Page == 0 && achiavements[j].Claimed == false && achiavements[j].Unlocked)
                {
                    if (achiavements[j].Order <= achiavements[LowestIndex].Order)
                    {
                        LowestIndex = j;
                        Found = true;
                    }
                }
            }

            if (Found)
            {
                Found = false;
                //Debug.Log("1");
                achiavements[LowestIndex].Page = Page;
                TempList.Add(achiavements[LowestIndex]);
                Index++;
                if (Index > MaxOnPage)
                {
                    Page++;
                    Index = 1;
                }
                LowestIndex = 4;
            }
        }

        Found = false;
        LowestIndex = 4;
        for (int i = 0; i < achiavements.Count; i++)
        {
            for (int j = 0; j < achiavements.Count; j++)
            {
                if (achiavements[j].Page == 0 && achiavements[j].Claimed == false && achiavements[j].Unlocked == false)
                {
                    if (achiavements[j].Order <= achiavements[LowestIndex].Order)
                    {
                        LowestIndex = j;
                        Found = true;
                    }
                }
            }

            if (Found)
            {
                Found = false;
                //Debug.Log(achiavements[LowestIndex].Order);
                achiavements[LowestIndex].Page = Page;
                TempList.Add(achiavements[LowestIndex]);
                Index++;
                if (Index > MaxOnPage)
                {
                    Page++;
                    Index = 1;
                }
                LowestIndex = 4;
            }
        }

        Found = false;
        LowestIndex = 4;
        for (int i = 0; i < achiavements.Count; i++)
        {
            for (int j = 0; j < achiavements.Count; j++)
            {
                if (achiavements[j].Page == 0 && achiavements[j].Claimed == true && achiavements[j].Unlocked)
                {
                    if (achiavements[j].Order <= achiavements[LowestIndex].Order)
                    {
                        LowestIndex = j;
                        Found = true;
                    }
                }
            }

            if (Found)
            {
                Found = false;
                //Debug.Log("3");
                achiavements[LowestIndex].Page = Page;
                TempList.Add(achiavements[LowestIndex]);
                Index++;
                if (Index > MaxOnPage)
                {
                    Page++;
                    Index = 1;
                }
                LowestIndex = 4;
            }
        }

        achiavements = TempList;
        MaxPage = Page;
    }

    public void CheckAll(bool Win)
    {
        for (int i = 0; i < achiavements.Count; i++)
        {
            if (achiavements[i].Unlocked == false)
            {
                achiavements[i].function(Win);
            }
        }
    }

    public void SaveAllToProgress()
    {
        for(int i = 0;i < achiavements.Count;i++)
        {
            achiavements[i].UpdateProgress();
        }
    }
}
