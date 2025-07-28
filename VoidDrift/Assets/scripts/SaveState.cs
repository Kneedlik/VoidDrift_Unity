using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SaveState : MonoBehaviour
{
    [SerializeField]  TMP_Text text;
    [SerializeField] TMP_Text Description;
    [SerializeField] TMP_Text SetTimeText;

    public bool Profile1;
    public bool Profile2;
    public bool Profile3;
    public bool Profile4;
    public bool Profile5;
    public bool Profile6;
    public bool Profile7;
    public bool Profile8;
    public bool Profile9;
    public bool Profile10;

    [SerializeField] List<TMP_Text> LevelTextList = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> DescriptionTextList = new List<TMP_Text>();

    private void OnEnable()
    {
        SetUpProfiles();
    }

    public void SaveExecute()
    {
        upgradeList[] Upgrades = FindObjectsOfType<upgradeList>();

        List<upgradeList> UpgradesList = new List<upgradeList>();
        for (int i = 0; i < Upgrades.Length; i++)
        {
            UpgradesList.Add(Upgrades[i]);
        }

        StateItem State = KnedlikLib.CreateUpgradeList(UpgradesList, Description.text);
        SaveManager.SaveState(text.text, State, false);

        SetUpProfiles();
    }

    public void LoadExecute()
    {
        string id = "";

        if (Profile1)
        {
            id = "1";
        } else if (Profile2)
        {
            id = "2";
        } else if (Profile3)
        {
            id = "3";
        }
        else if (Profile4)
        {
            id = "4";
        }
        else if (Profile5)
        {
            id = "5";
        }
        else if (Profile6)
        {
            id = "6";
        }
        else if (Profile7)
        {
            id = "7";
        }
        else if(Profile8)
        {
            id = "8";
        }
        else if(Profile9)
        {
            id = "9";
        }
        else if(Profile10)
        {
            id = "10";
        }

        StateItem ChoosenProfile = SaveManager.LoadState(id,false);
        if ( ChoosenProfile == null )
        {
            Debug.Log("Nefacha");
            return;
        }
        
        PlayerStats.sharedInstance.ResetUpgrades();

        upgradeList[] Upgrades = FindObjectsOfType<upgradeList>();
        List<int> UsedUpgrades = new List<int>();
        for (int i = 0; i < Upgrades.Length; i++)
        {
            //Debug.Log("1");
            for (int j = 0; j < Upgrades[i].list.Count; j++)
            {
                //Debug.Log("2");
                for (int k = 0; k < ChoosenProfile.UpgradeItems.Count; k++)
                {
                    //Debug.Log("3");
                    if (Upgrades[i].list[j].upgrade.Id == ChoosenProfile.UpgradeItems[k].Id && UsedUpgrades.Contains(Upgrades[i].list[j].upgrade.Id) == false)
                    {
                        for (int l = 0; l < ChoosenProfile.UpgradeItems[k].Level; l++)
                        {
                            Debug.Log(Upgrades[i].list[j].upgrade.Id);
                            levelingSystem.instance.IncreaseLevel();
                            levelingSystem.instance.ScaleXp();
                            Upgrades[i].list[j].upgrade.function();
                            PlayerStats.sharedInstance.UpdateStats();
                        }
                        ChoosenProfile.UpgradeItems.RemoveAt(k);
                        UsedUpgrades.Add(Upgrades[i].list[j].upgrade.Id);
                        break;
                    }
                }
            }
        }
    }

    public void SetUpProfiles()
    {
        for (int i = 1; i < 11; i++)
        {
            int j = i - 1;
            StateItem ChoosenProfile = SaveManager.LoadState(i.ToString(), false);
            if(ChoosenProfile != null)
            {
                LevelTextList[j].text = string.Format("Level: {0}", ChoosenProfile.Level);
                DescriptionTextList[j].text = ChoosenProfile.Description;
            }else
            {
                LevelTextList[j].text = "";
                DescriptionTextList[j].text = "";
            }
        }
    }

    public void SetTimeExecute()
    {
        //timer.instance.gameTime = KnedlikLib.ConvertStringTimeToFloat("12:30");
        Debug.Log(SetTimeText.text);
        timer.instance.gameTime = KnedlikLib.ConvertStringTimeToFloat(SetTimeText.text);

        Debug.Log(timer.instance.gameTime);

    }

    public void ChangeProfileCheck1(bool value)
    {
        Profile1 = value;
    }

    public void ChangeProfileCheck2(bool value)
    {
        Profile2 = value;
    }

    public void ChangeProfileCheck3(bool value)
    {
        Profile3 = value;
    }

    public void ChangeProfileCheck4(bool value)
    {
        Profile4 = value;
    }

    public void ChangeProfileCheck5(bool value)
    {
        Profile5 = value;
    }

    public void ChangeProfileCheck6(bool value)
    {
        Profile6 = value;
    }

    public void ChangeProfileCheck7(bool value)
    {
        Profile7 = value;
    }

    public void ChangeProfileCheck8(bool value)
    {
        Profile8 = value;
    }

    public void ChangeProfileCheck9(bool value)
    {
        Profile9 = value;
    }

    public void ChangeProfileCheck10(bool value)
    {
        Profile10 = value;
    }

    public void Cancel()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
