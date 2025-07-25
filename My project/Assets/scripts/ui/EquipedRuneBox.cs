using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquipedRuneBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int SlotId;
    public int EquipedId;
    public bool Filled;
    public bool Selected;
    public bool KeyStone;
    public bool Unlocked;
    [SerializeField] GameObject LockedObj;
    public Image Icon;
    public Image SelectedImg;
    public TMP_Text DescriptionText;
    public GameObject DescriptionObj;

    // Start is called before the first frame update
    void Start()
    {
        Deselect();
        LoadFromProgression();
        LoadFromPrefs();
    }

    public void LoadFromProgression()
    {
        if(PlayerMenuManager.instance.progressionState.UnlockedSlots.Contains(SlotId))
        {
            Unlock();
        }else Lock();
    }

    public void Unlock()
    {
        Unlocked = true;
        LockedObj.SetActive(false);
    }

    public void Lock()
    {
        Unlocked = false;
        LockedObj.SetActive(true);
    }

    public void LoadFromPrefs()
    {
        if(Unlocked)
        {
            int Temp;
            switch(SlotId)
            {
                case 1: Temp = PlayerMenuManager.instance.playerPrefs.Keystone;
                    break;
                case 2: Temp = PlayerMenuManager.instance.playerPrefs.RuneSlot1;
                    break;
                case 3: Temp = PlayerMenuManager.instance.playerPrefs.RuneSlot2;
                    break;
                case 4: Temp = PlayerMenuManager.instance.playerPrefs.RuneSlot3;
                    break;
                case 5: Temp = PlayerMenuManager.instance.playerPrefs.RuneSlot4;
                    break;
                case 6: Temp = PlayerMenuManager.instance.playerPrefs.RuneSlot5;
                    break;
                default: Temp = 0; 
                    break;
            }

            if(Temp == 0)
            {
                Icon.gameObject.SetActive(false);
            }else
            {
                Icon.gameObject.SetActive(true);
                for (int i = 0; i < PlayerMenuManager.instance.RuneBoxes.Count; i++)
                {
                    if (PlayerMenuManager.instance.RuneBoxes[i].Id == Temp)
                    {
                        PlayerMenuManager.instance.SelectedBox = SlotId;
                        PlayerMenuManager.instance.RuneBoxes[i].Pick();
                    }
                }
            }
        }else
        {
            Icon.gameObject.SetActive(false);
        }

        PlayerMenuManager.instance.SelectedBox = 0;
    }

    public void Select()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        if (Unlocked)
        {
            PlayerMenuManager.instance.DeselectAllEquiped();
            PlayerMenuManager.instance.SelectedBox = SlotId;
            SelectedImg.color = new Color32(255, 100, 0, 255);
            Selected = true;
        }
    }

    public void Deselect()
    {
        Selected = false;
        SelectedImg.color = new Color32(0,65,255,255);  
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Unlocked && EquipedId != 0)
        {
            DescriptionObj.SetActive(true);
        }

        SelectedImg.color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptionObj.SetActive(false);

        if(Selected)
        {
            SelectedImg.color = new Color32(255,100,0,255);
        }else
        {
            SelectedImg.color = new Color32(0, 65, 255, 255);
        }
    }
}
