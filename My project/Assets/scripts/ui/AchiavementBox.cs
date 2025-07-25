using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AchiavementBox : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler
{
    public int Id;
    public Image Icon;
    public Image RewardIcon;
    public Image Border;
    public Image Background;
    public TMP_Text Description;
    public TMP_Text Progress;
    public Slider ProgressSlider;
    public TMP_Text RewardText;

    public void LoadAchievement(Achiavement achievement)
    {
        Id = achievement.Id;

        if (Icon != null)
        {
            Icon.enabled = true;
            Icon.sprite = achievement.Icon;
            Icon.color = achievement.IconColor;
        }else
        {
            Icon.enabled = false;
        }
        if(achievement.RewardIcon != null)
        {
            RewardIcon.enabled = true;
            RewardIcon.sprite = achievement.RewardIcon;
            RewardIcon.color = achievement.RewardIconColor;
        }else
        {
            RewardIcon.enabled = false;
        }
        Description.text = achievement.Description;
        Progress.text = string.Format("{0} / {1}", achievement.Current, achievement.Needed);
        ProgressSlider.maxValue = achievement.Needed;
        ProgressSlider.value = achievement.Current;
        RewardText.text = achievement.SecondText;
        if(achievement.BackGround != null)
        {
            Background.enabled = true;
            Background.sprite = achievement.BackGround;
        }else
        {
            Background.enabled = false;    
        }

        if( achievement.Claimed == false && achievement.Unlocked)
        {
            Border.color = new Color32(255, 175, 0, 255);
            //achievement.Claimed = true;
        }else
        {
            Border.color = new Color32(0, 65, 255, 255);
        }
    }

    public void UnloadAchiavement()
    {
        Id = 0;
        Icon.enabled = false;
        Background.enabled = false;
        RewardIcon.enabled = false;
        Description.text = "";
        Progress.text = "";
        RewardText.text = "";
        ProgressSlider.value = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Border.color = new Color32(255,255,255,255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Border.color = new Color32(0,65,255,255);
    }
}
