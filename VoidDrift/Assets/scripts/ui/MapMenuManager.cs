using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MapMenuManager : MonoBehaviour
{
    public List<LevelSelectionBox> LevelBoxes = new List<LevelSelectionBox>();
    public int SelectedLevel = 0;
    [SerializeField] GameObject LockedObj;
    [SerializeField] ProgressionState Progress;
    [SerializeField] PlayerInformation PlayerInfo;
    [SerializeField] PlayerPrefs Prefs;
    [SerializeField] Toggle toggle;
    public bool HardMoodeUnlocked;

    private void Start()
    {
        SelectedLevel = 0;

        if(Progress.HardModeUnlocked)
        {
            HardMoodeUnlocked = true;
            LockedObj.SetActive(false);
            if(Prefs.HardMode)
            {
                toggle.isOn = true;
                PlayerInfo.HardMode = true;
            }else
            {
                toggle.isOn = false;
                PlayerInfo.HardMode = false;
            }
        }else
        {
            toggle.isOn = false;
            toggle.interactable = false;

            HardMoodeUnlocked = false;
            LockedObj.SetActive(true);

            PlayerInfo.HardMode = false;
        }
    }

    public void StartLevel()
    {
        int LevelId = SelectedLevel;
        if(LevelId == 1)
        {
            if(PlayerInfo.HardMode)
            {
                PlayerInfo.MapGoldMultiplier = ProgressionConst.Level1GoldHard;
                PlayerInfo.MapEnemyHealthMultiplier = ProgressionConst.Level1HealthHard;
                PlayerInfo.MapDamageMultiplier = ProgressionConst.Level1DamageHard;
            }else
            {
                PlayerInfo.MapGoldMultiplier = ProgressionConst.Level1Gold;
                PlayerInfo.MapEnemyHealthMultiplier = ProgressionConst.Level1Health;
                PlayerInfo.MapDamageMultiplier = ProgressionConst.Level1Damage;
            }

            SceneManager.LoadScene(3);
        }
        else if(LevelId == 2)
        {
            if(PlayerInfo.HardMode)
            {
                PlayerInfo.MapGoldMultiplier = ProgressionConst.Level2GoldHard;
                PlayerInfo.MapEnemyHealthMultiplier = ProgressionConst.Level2HealthHard;
                PlayerInfo.MapDamageMultiplier = ProgressionConst.Level2DamageHard;
            }
            else
            {
                PlayerInfo.MapGoldMultiplier = ProgressionConst.Level2Gold;
                PlayerInfo.MapEnemyHealthMultiplier = ProgressionConst.Level2Health;
                PlayerInfo.MapDamageMultiplier = ProgressionConst.Level2Damage;
            }

            SceneManager.LoadScene(4);
        }
        else if(LevelId == 3)
        {
            if(PlayerInfo.HardMode)
            {
                PlayerInfo.MapGoldMultiplier = ProgressionConst.Level3GoldHard;
                PlayerInfo.MapEnemyHealthMultiplier = ProgressionConst.Level3HealthHard;
                PlayerInfo.MapDamageMultiplier = ProgressionConst.Level3DamageHard;
            }
            else
            {
                PlayerInfo.MapGoldMultiplier = ProgressionConst.Level3Gold;
                PlayerInfo.MapEnemyHealthMultiplier= ProgressionConst.Level3Health;
                PlayerInfo.MapDamageMultiplier = ProgressionConst.Level3Damage;
            }

            SceneManager.LoadScene(5);
        }
    }

    public void ExitToPlayerMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void DeselectAll()
    {
        for(int i = 0; i < LevelBoxes.Count; i++)
        {
            SelectedLevel = 0;
            LevelBoxes[i].DeselectLevel();
        }
    }

    public void HardModeToggle(bool IsHardMode)
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }

        Debug.Log(IsHardMode);
        if(IsHardMode)
        {
            ActivateHardMode();
        }else
        {
            DeactivateHardMode();
        }
    }


    public void ActivateHardMode()
    {
        if (HardMoodeUnlocked)
        {
            PlayerInfo.HardMode = true;
            Prefs.HardMode = true;
        }
    }

    public void DeactivateHardMode()
    {
        if (HardMoodeUnlocked)
        {
            PlayerInfo.HardMode = false;
            Prefs.HardMode = false;
        }
    }

}
