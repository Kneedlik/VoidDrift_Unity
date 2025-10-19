using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class WeapeonBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int WeapeonId;
    public GameObject SelectedObject;
    public bool Unlocked;
    public bool Bought;
    public int Price;
    [SerializeField] Image WeapeonImage;
    [SerializeField] GameObject LockedObj;
    [SerializeField] PlayerInformation playerInformation;
    [SerializeField] PlayerMenuManager playerMenuManager;
    [SerializeField] TMP_Text PriceText;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < playerMenuManager.progressionState.UnlockedWeapeons.Count; i++)
        {
            if (playerMenuManager.progressionState.UnlockedWeapeons[i].Id == WeapeonId)
            {
                if (playerMenuManager.progressionState.UnlockedWeapeons[i].Unlocked)
                {
                    UnLock();
                    if (playerMenuManager.progressionState.UnlockedWeapeons[i].Bought)
                    {
                        Bought = true;
                        WeapeonImage.color = new Color32(255, 255, 255, 255);
                    }else
                    {
                        Bought = false;
                        WeapeonImage.color = new Color32(255, 255, 255, 125);
                    }
                    return;
                }
            }
        }

        Lock();
    }

    public void SelectWeapeon()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        //Debug.Log("222");
        if (Unlocked)
        {
            if(Bought == false)
            {
                Buy();
                return;
            }

            playerMenuManager.DeselectAllWeapeons();
            playerInformation.WeapeonId = WeapeonId;
            //Debug.Log(WeapeonId);
            SelectedObject.SetActive(true);
        }
    }

    public void DeSelectWeapeon()
    {
        SelectedObject.SetActive(false);
    }

    public void Lock()
    {
        Unlocked = false;
        LockedObj.SetActive(true);
    }

    public void UnLock()
    {
        Unlocked = true;
        LockedObj.SetActive(false);
    }

    public void Buy()
    {
        if(Unlocked == false)
        {
            return;
        }

        if(Bought)
        {
            return;
        }

        if (playerMenuManager.progressionState.Gold - Price > 0)
        {
            playerMenuManager.progressionState.Gold -= Price;
            playerMenuManager.Counter.SetCounter(playerMenuManager.progressionState.Gold);

            Bought = true;
            bool Contains = false;
            for (int i = 0; i < playerMenuManager.progressionState.UnlockedWeapeons.Count; i++)
            {
                if (playerMenuManager.progressionState.UnlockedWeapeons[i].Id == WeapeonId)
                {
                    playerMenuManager.progressionState.UnlockedWeapeons[i].Unlocked = true;
                    playerMenuManager.progressionState.UnlockedWeapeons[i].Bought = true;
                }
            }

            if(Contains == false)
            {
                WeapeonState State = new WeapeonState();
                State.Id = WeapeonId;
                State.Unlocked = true;
                State.Bought = true;
            }

            WeapeonImage.color = new Color32(255, 255, 255, 255);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PriceText.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Unlocked == false)
        {
            return;
        }

        if(Bought)
        {
            return;
        }

        PriceText.gameObject.SetActive(true);
        PriceText.text = Price.ToString();
    }
}
