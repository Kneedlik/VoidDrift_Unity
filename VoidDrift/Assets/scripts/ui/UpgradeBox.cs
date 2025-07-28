using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UpgradeBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int Id;
    public int MaxLevel;
    public int CurrentLevel;
    public bool Unlocked;
    public bool UnlockedByDefault;
    public bool IsPercentIncrease;
    [SerializeField] GameObject LockedObj;
    [SerializeField] GameObject LockedShadow;
    [SerializeField] List<int> Prices = new List<int>();
    [SerializeField] List<Image> LevelImages = new List<Image>();
    [SerializeField] TMP_Text Price;
    [SerializeField] TMP_Text Name;
    [SerializeField] Image Icon;

    // Start is called before the first frame update
    void Start()
    {
        LoadFromProgression();
    }

    public void LoadFromProgression()
    {
        Unlocked = false;
        CurrentLevel = 0;

        for (int i = 0;i < ShopManager.Instance.Progress.ShopUpgradesProgression.Count;i++)
        {
            if (ShopManager.Instance.Progress.ShopUpgradesProgression[i].Id == Id)
            {
                Unlocked = ShopManager.Instance.Progress.ShopUpgradesProgression[i].Unlocked;
                CurrentLevel = ShopManager.Instance.Progress.ShopUpgradesProgression[i].CurrentLevel;
            }
        }

        if (Unlocked || UnlockedByDefault)
        {
            Unlock();
        }
        else
        {
            Lock();
        }

        SetPrice();

        for (int i = 0; i < LevelImages.Count; i++) 
        {
            if(CurrentLevel > i)
            {
                UpgradeConsts.SetColorFill(LevelImages[i]);
            }else
            {
                UpgradeConsts.SetColorToEmply(LevelImages[i]);
            }
        
        }
    }

    public void LevelUp()
    {
        if(CurrentLevel >= MaxLevel)
        {
            return;
        }

        if (Unlocked || UnlockedByDefault)
        {
            if (Prices.Count > CurrentLevel)
            {
                if (ShopManager.Instance.Progress.Gold >= Prices[CurrentLevel])
                {
                    ShopManager.Instance.Progress.Gold -= Prices[CurrentLevel];
                    ShopManager.Instance.goldCounter.UpdateCount();
                    CurrentLevel += 1;
                    IncreaseLevel();

                }
            }else
            {
                CurrentLevel += 1;
                IncreaseLevel();
            }
        }else
        {
            IncreaseLevel();
        }

        LoadFromProgression();
        SetUpWindow();
        ShopManager.Instance.PlayerInfo.CalculateStats(ShopManager.Instance.Progress);
    }

    public void IncreaseLevel()
    {
        //if(CurrentLevel == 0)
        //{
        //    return;
        //}

        for (int i = 0;i < ShopManager.Instance.Progress.ShopUpgradesProgression.Count; i++)
        {
            if (ShopManager.Instance.Progress.ShopUpgradesProgression[i].Id == Id)
            {
                ShopManager.Instance.Progress.ShopUpgradesProgression[i].CurrentLevel = CurrentLevel;
                return;
            }
        }

        ShopUpgradeState Temp = new ShopUpgradeState();
        Temp.CurrentLevel = CurrentLevel;
        Temp.Unlocked = true;
        Temp.Id = Id;
        ShopManager.Instance.Progress.ShopUpgradesProgression.Add(Temp);
    }

    public void Lock()
    {
        LockedObj.SetActive(true);
        LockedShadow.SetActive(true);
    }

    public void Unlock()
    {
        LockedObj.SetActive(false);
        LockedShadow.SetActive(false);
    }

    public void SetPrice()
    {
        if (Unlocked || UnlockedByDefault)
        {
            if (CurrentLevel < MaxLevel)
            {
                if (Prices.Count >= CurrentLevel && Prices.Count != 0)
                {
                    Price.text = Prices[CurrentLevel].ToString();
                }
                else Price.text = "";
            }
            else
            {
                Price.text = "Max";
            }
        }else
        {
            Price.text = "";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetUpWindow();
    }

    public void SetUpWindow()
    {
        ShopManager.Instance.WindowObj.SetActive(true);

        ShopManager.Instance.Name.text = Name.text;
        ShopManager.Instance.WindowIcon.sprite = Icon.sprite;
        ShopManager.Instance.WindowIcon.color = Icon.color;

        float Temp1;
        float Temp2;
        Temp1 = CurrentLevel * UpgradeConsts.GetIncrease(Id);
        Temp2 = Temp1 + UpgradeConsts.GetIncrease(Id);
        if (IsPercentIncrease)
        {
            ShopManager.Instance.IncreaseText.text = string.Format("+{0}% > +{1}%", Temp1, Temp2);
        }
        else ShopManager.Instance.IncreaseText.text = string.Format("+{0} > +{1}", Temp1, Temp2);

        ShopManager.Instance.SetWindowDescription(Id);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShopManager.Instance.HideWindow();
    }

}
