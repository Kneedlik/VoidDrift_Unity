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
    int UpgradeType;

    void Start()
    {
        CurrentRerolls = MasterManager.Instance.PlayerInformation.Rerols + 1;
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
        if(CurrentRerolls <= 0)
        {
            return;
        }else
        {
            CurrentRerolls = CurrentRerolls - 1;
            text.text = CurrentRerolls.ToString();
            if (CurrentRerolls <= 0)
            {
                Lock();
            }
        }

        UpgradeType = 1;
        for (int i = 0; i < Sorting.cards.Count; i++)
        {
            if (Sorting.cards[i].gameObject.activeSelf)
            {
                if (Sorting.cards[i].Upgrade.upgrade.Type == type.iron)
                {
                    UpgradeType = 2;
                }
                else if (Sorting.cards[i].Upgrade.upgrade.Type == type.currupted)
                {
                    UpgradeType = 3;
                }
            }
        }

        if (UpgradeType == 1)
        {
            Sorting.setUpCards();
        }else if(UpgradeType == 2)
        {
            NonLevelUpgradeSorting SortingTemp = GameObject.FindWithTag("IronUpgrades").GetComponent<NonLevelUpgradeSorting>();
            levelingSystem.instance.SetUpLevelMenu(true, SortingTemp);
        }
        else if(UpgradeType == 3)
        {
            NonLevelUpgradeSorting SortingTemp = GameObject.FindWithTag("CorruptedUpgrades").GetComponent<NonLevelUpgradeSorting>();
            levelingSystem.instance.SetUpLevelMenu(true, SortingTemp);
        }

        for (int i = 0; i < Sorting.cards.Count; i++)
        {
            if (Sorting.cards[i].gameObject.activeSelf)
            {
                Sorting.cards[i].UpdateUI();
            }
        }
    }
}
