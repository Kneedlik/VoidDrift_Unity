using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RerollManager : MonoBehaviour
{
    public int MaxRerolls = 9;
    public int CurrentRerolls;
    public upgradeSorting Sorting;
    [SerializeField] GameObject LockedObj;
    [SerializeField] TMP_Text text;

    void Start()
    {
        CurrentRerolls = MasterManager.Instance.PlayerInformation.Rerols;
        text.text = CurrentRerolls.ToString();
        if(CurrentRerolls <= 0)
        {
            Lock();
        }else Unlock();
    }

    public void AddReroll()
    {
        if(CurrentRerolls < MaxRerolls)
        {
            CurrentRerolls++;
            text.text = CurrentRerolls.ToString();
        }

        if (CurrentRerolls <= 0)
        {
            Lock();
        }
        else Unlock();
    }

    public void Lock()
    {
        LockedObj.SetActive(true);
    }

    public void Unlock()
    {
        LockedObj.SetActive(false);
    }

    public void ReRollUpgrades()
    {
        //if(CurrentRerolls <= 0)
        //{
        //    return;
        //}else
        //{
            CurrentRerolls = CurrentRerolls - 1;
            text.text = CurrentRerolls.ToString();
            if (CurrentRerolls <= 0)
            {
                Lock();
            }
        //}

        Sorting.setUpCards();
        for (int i = 0; i < Sorting.cards.Count; i++)
        {
            if (Sorting.cards[i].gameObject.activeSelf)
            {
                Sorting.cards[i].UpdateUI();
            }
        }
    }
}
