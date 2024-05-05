using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class upgradeDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public upgrade Upgrade;
    public TMP_Text Name;
    public TMP_Text Description;
    public Image image;
    public Image Insignia;
    public Image SelectedImg;
    Button button;
    upgradeSorting sorting;

    private void Start()
    {
        button = GetComponent<Button>();
        sorting = GameObject.FindWithTag("MainUpgrades").GetComponent<upgradeSorting>();
    }

    public void activate()
    {
        if(levelingSystem.instance.Double)
        {
            Upgrade.function();
            Upgrade.level -= 1;
            Upgrade.function();
            levelingSystem.instance.Double = false;
        }else
        {
            Upgrade.function();
        }

        if (PlayerStats.sharedInstance.ownedColours.Count < 3)
        {
            if(PlayerStats.sharedInstance.ownedColours.Count == 0)
            {
                PlayerStats.sharedInstance.ownedColours.Add(Upgrade.Type);
            }
            else
            {
                bool matching = false;
                for (int i = 0; i < PlayerStats.sharedInstance.ownedColours.Count; i++)
                {
                    if (PlayerStats.sharedInstance.ownedColours[i] == Upgrade.Type)
                    {
                        matching = true; 
                    }
               }
                if(matching == false)
                {
                    PlayerStats.sharedInstance.ownedColours.Add(Upgrade.Type);
                }

            }   
        }

        if(sorting != null)
        {
            sorting.PunishNotChoosen(Upgrade);
        }

        if(Upgrade.Type == type.red)
        {
            levelingSystem.instance.red++;
        }else if(Upgrade.Type == type.green)
        {
            levelingSystem.instance.green++;
        }else if(Upgrade.Type==type.blue)
        {
            levelingSystem.instance.blue++;
        }else if(Upgrade.Type == type.purple)
        {
            levelingSystem.instance.purple++;
        }else if(Upgrade.Type == type.yellow)
        {
            levelingSystem.instance.yellow++;
        }
    }

    private void Update()
    {
        Name.text = Upgrade.name;
        Description.text = Upgrade.description;
        image.sprite = Upgrade.icon;
        ColorBlock cb = button.colors;
        cb.normalColor = Upgrade.color;
        cb.disabledColor = Upgrade.color;
        button.colors = cb;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.white;
        SelectedImg.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Upgrade.color;
        SelectedImg.enabled = false;
    }

    private void OnEnable()
    {
        image.color = Upgrade.color;
        SelectedImg.enabled = false;
    }


}
