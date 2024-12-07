using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollManager : MonoBehaviour
{
    public int MaxRerolls;
    public int CurrentRerolls;
    public upgradeSorting Sorting;

    void Start()
    {
        
    }

    public void ReRollUpgrades()
    {
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
