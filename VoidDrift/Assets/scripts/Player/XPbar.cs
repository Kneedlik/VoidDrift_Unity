using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPbar : MonoBehaviour
{
   public Slider slider;
   public TextMeshProUGUI textMeshProUGUI;
   
   public void setXP(int xp)
    {
        slider.value = xp;
    }

   public void setMaxXp(int xp)
    {
        slider.maxValue = xp;
    }

    public void displayedLevel(int level)
    {
        //  textMeshProUGUI.text = level.ToString();
        textMeshProUGUI.text = string.Format("Level {0}", level);
    }
}
