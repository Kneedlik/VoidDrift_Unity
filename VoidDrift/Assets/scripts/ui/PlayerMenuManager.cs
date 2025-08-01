using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayerMenuManager : MonoBehaviour
{
    public int SelectedBox;
    public PlayerInformation playerInformation;
    public PlayerPrefs playerPrefs;
    public ProgressionState progressionState;
    public static PlayerMenuManager instance;
    public List<WeapeonBox> weapeonBoxes = new List<WeapeonBox>();
    public List<RuneBox> RuneBoxes = new List<RuneBox>();
    public List<EquipedRuneBox> EquipedRuneBoxes = new List<EquipedRuneBox>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DeselectAllWeapeons();
        //LockAllWeapeons();
        LoadFromPlayerPrefs();

        for (int i = 0; i < RuneBoxes.Count; i++)
        {
            RuneBoxes[i].LoadFromProggression();
        }

        for (int i = 0;i < EquipedRuneBoxes.Count; i++)
        {
            EquipedRuneBoxes[i].Deselect();
            EquipedRuneBoxes[i].LoadFromProgression();
            EquipedRuneBoxes[i].LoadFromPrefs();
        }
    }

    public void LoadFromPlayerPrefs()
    {
        /*
        WeapeonId.text = playerInformation.WeapeonId.ToString();
        DamageMultiplier.text = playerInformation.DamageMultiplier.ToString();
        AsMultiplier.text = playerInformation.AsMultiplier.ToString();
        HealthBonus.text = playerInformation.HealthBonus.ToString();
        MsMultiplier.text = playerInformation.MsMultiplier.ToString();
        XpMultiplier.text = playerInformation.XpMultiplier.ToString();
        SizeMultiplier.text = playerInformation.SizeMultiplier.ToString();
        ReviveBonus.text = playerInformation.ReviveBonus.ToString();
        ProjectileBonus.text = playerInformation.ProjectileBonus.ToString();
        SummonDamageMultiplier.text = playerInformation.SummonDamageMultiplier.ToString();
        */

        for (int i = 0; i < weapeonBoxes.Count; i++)
        {
            if (weapeonBoxes[i].WeapeonId == playerInformation.WeapeonId)
            {
                weapeonBoxes[i].SelectWeapeon();
            }
        }
    }

    public void SaveToPlayerPrefs()
    {
        playerPrefs.EquipedWeapeon = playerInformation.WeapeonId;
        for (int i = 0;i < EquipedRuneBoxes.Count;i++)
        {
            switch(EquipedRuneBoxes[i].SlotId)
            {
                case 1: playerPrefs.Keystone = EquipedRuneBoxes[i].EquipedId;
                    break;
                case 2: playerPrefs.RuneSlot1 = EquipedRuneBoxes[i].EquipedId;
                    break;
                case 3: playerPrefs.RuneSlot2 = EquipedRuneBoxes[i].EquipedId;
                    break;
                case 4: playerPrefs.RuneSlot3 = EquipedRuneBoxes[i].EquipedId;
                    break;
                case 5: playerPrefs.RuneSlot4 = EquipedRuneBoxes[i].EquipedId;
                    break;
                case 6: playerPrefs.RuneSlot5 = EquipedRuneBoxes[i].EquipedId;
                    break;
            }
        }
    }

    public void SwitchWeapeon(int Id)
    {
        playerInformation.WeapeonId = Id;
    }

    public void AdvanceToLevelSelection()
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        playerInformation.CalculateStats(progressionState);
        SaveToPlayerPrefs();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DeselectAllWeapeons()
    {
        for (int i = 0;i < weapeonBoxes.Count;i++)
        {
            weapeonBoxes[i].DeSelectWeapeon();
        }
    }

    public void LockAllWeapeons()
    {
        for (int i = 0; i < weapeonBoxes.Count; i++)
        {
            weapeonBoxes[i].Lock();
        }
    }

    public void ExitToStartMenu()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        SaveManager.SavePlayerPrefs(playerPrefs);
        SaveManager.SavePlayerProgress(progressionState);
        SaveToPlayerPrefs();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void EnterShop()
    {
        if (AudioManager.instance != null)
        { 
            AudioManager.instance.PlayId(10);
        }
        SaveToPlayerPrefs();
        SceneManager.LoadScene(6);
    }

    public void EnterAchievments()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        SaveToPlayerPrefs();
        SceneManager.LoadScene(7);
    }

    public void DeselectAllEquiped()
    {
        for(int i = 0; i < EquipedRuneBoxes.Count; i++)
        {
            EquipedRuneBoxes[i].Deselect();
            SelectedBox = 0;
        }
    }
}
