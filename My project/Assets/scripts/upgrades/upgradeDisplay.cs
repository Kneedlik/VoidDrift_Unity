using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
public class HintGroup
{
    public GameObject Group;
    public Image Icon;
    public TMP_Text Text;
}

public class upgradeDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UpgradePlus Upgrade;
    public TMP_Text Name;
    public TMP_Text Description;
    public Image image;
    public Image Insignia;
    public Image SelectedImg;
    Button button;
    upgradeSorting sorting;
    [SerializeField] GameObject HintWindow;
    [SerializeField] Image Panel;
    [SerializeField] List<HintGroup> Groups = new List<HintGroup>();
    [SerializeField] List<GameObject> Arrows = new List<GameObject>();
    [SerializeField] Image MainUpgradeImage;
    [SerializeField] Sprite SpriteShort;
    [SerializeField] Sprite SpriteShortFill;
    [SerializeField] Sprite SpriteLong;
    [SerializeField] Sprite SpriteLongFill;
    //float BasePosY;
    float BaseHeight;
    int Diff;

    private void Start()
    {
        RectTransform rect = HintWindow.GetComponent<RectTransform>();
        BaseHeight = rect.sizeDelta.y;

        button = GetComponent<Button>();
        sorting = GameObject.FindWithTag("MainUpgrades").GetComponent<upgradeSorting>();
    }

    public void activate()
    {
        if(levelingSystem.instance.Double)
        {
            Upgrade.upgrade.function();
            Upgrade.upgrade.level -= 1;
            Upgrade.upgrade.function();
            levelingSystem.instance.Double = false;
        }else
        {
            Upgrade.upgrade.function();
        }

        if(Upgrade.upgrade.Type == type.currupted || Upgrade.upgrade.Type == type.iron || Upgrade.upgrade.Type == type.special)
        {
            return;
        }

        if (PlayerStats.sharedInstance.ownedColours.Count < 3)
        {
            if(PlayerStats.sharedInstance.ownedColours.Count == 0)
            {
                PlayerStats.sharedInstance.ownedColours.Add(Upgrade.upgrade.Type);
            }
            else
            {
                bool matching = false;
                for (int i = 0; i < PlayerStats.sharedInstance.ownedColours.Count; i++)
                {
                    if (PlayerStats.sharedInstance.ownedColours[i] == Upgrade.upgrade.Type)
                    {
                        matching = true; 
                    }
               }
                if(matching == false)
                {
                    PlayerStats.sharedInstance.ownedColours.Add(Upgrade.upgrade.Type);
                }

            }   
        }

        if(sorting != null)
        {
            sorting.PunishNotChoosen(Upgrade.upgrade);
        }

        if(Upgrade.upgrade.Type == type.red)
        {
            levelingSystem.instance.red++;
        }else if(Upgrade.upgrade.Type == type.green)
        {
            levelingSystem.instance.green++;
        }else if(Upgrade.upgrade.Type==type.blue)
        {
            levelingSystem.instance.blue++;
        }else if(Upgrade.upgrade.Type == type.purple)
        {
            levelingSystem.instance.purple++;
        }else if(Upgrade.upgrade.Type == type.yellow)
        {
            levelingSystem.instance.yellow++;
        }
    }

    private void Update()
    {
        if (Upgrade.upgrade.level > 0)
        {
            Name.text = Upgrade.upgrade.name + string.Format(" {0}", Upgrade.upgrade.level + 1);
        } else
        {
            Name.text = Upgrade.upgrade.name;
        }
        Description.text = Upgrade.upgrade.description;
        image.sprite = Upgrade.upgrade.icon;
        ColorBlock cb = button.colors;
        cb.normalColor = Upgrade.upgrade.color;
        cb.disabledColor = Upgrade.upgrade.color;
        button.colors = cb;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.white;
        SelectedImg.enabled = true;
        SelectedImg.color = Color.white;
        if (Upgrade.upgrade.Final)
        {
            image.color = new Color32(255, 180, 0, 255);
            SelectedImg.color = new Color32(255, 180, 0, 255);
        }

        if (Upgrade.SuperiorUpgrade != null)
        {
            //Debug.Log(Upgrade.SuperiorUpgrade.name);
            ShowHint();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Upgrade.upgrade.color;
        if(Upgrade.upgrade.Final)
        {
            image.color = SelectedImg.color = new Color32(255, 180, 0, 255);
        }

        SelectedImg.enabled = false;

        if (Upgrade.SuperiorUpgrade != null)
        {
            HideHint();
        }
    }

    public void UpdateUI()
    {
        if (Upgrade != null && image != null)
        {
            image.color = Upgrade.upgrade.color;
            if (Upgrade.upgrade.Final)
            {
                image.color = new Color32(255, 180, 0, 255);
            }
            SelectedImg.enabled = false;
        }

        if (HintWindow != null)
        {
            HintWindow.SetActive(false);
        }
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void ShowHint()
    {
        HideHint();
        HintWindow.SetActive(true);
        MainUpgradeImage.sprite = Upgrade.SuperiorUpgrade.icon;
        RectTransform rect = HintWindow.GetComponent<RectTransform>();
        
        Diff = 3 - Upgrade.subserviantUpgrades.Count;
        Vector2 Height = new Vector2(rect.sizeDelta.x,rect.sizeDelta.y);

        for (int j = 0; j < Diff; j++)
        {
            Height.y -= 38;
        }
        rect.sizeDelta = Height;

        for (int i = 0; i < Upgrade.subserviantUpgrades.Count; i++)
        {
            Groups[i].Group.SetActive(true);
            Groups[i].Icon.sprite = Upgrade.subserviantUpgrades[i].Upgrade.icon;
            Groups[i].Text.text = Upgrade.subserviantUpgrades[i].Upgrade.level.ToString() + " / " + Upgrade.subserviantUpgrades[i].LevelNeeded.ToString();
            
            //Groups[i].Text.text = 
        }

        Image ImageTemp = HintWindow.GetComponent<Image>();
        switch (Upgrade.subserviantUpgrades.Count)
        {
            case 1:
                Arrows[0].SetActive(true);
                break;
            case 2:
                Arrows[1].SetActive(true);
                break;
            case 3:
                Arrows[2].SetActive(true);
                break;
        }
    }

    public void HideHint()
    {
        RectTransform rect = HintWindow.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, BaseHeight);

        for (int i = 0; i < Groups.Count; i++)
        {
            Groups[i].Group.SetActive(false);
        }

        for (int j = 0;j < Arrows.Count; j++)
        {
            Arrows[j].SetActive(false);
        }

        HintWindow.SetActive(false);
    }
}
