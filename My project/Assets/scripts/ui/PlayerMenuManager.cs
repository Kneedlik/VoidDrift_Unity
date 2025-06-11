using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PlayerMenuManager : MonoBehaviour
{
    [SerializeField] PlayerInformation playerInformation;
    public List<WeapeonBox> weapeonBoxes = new List<WeapeonBox>();

    // Start is called before the first frame update
    void Start()
    {
        DeselectAllWeapeons();
        //LockAllWeapeons();
        LoadFromPlayerInfo();
    }

    public void LoadFromPlayerInfo()
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
                Debug.Log("111");
                weapeonBoxes[i].SelectWeapeon();
            }
        }
    }

    public void SaveToPlayerInfo()
    {
        /*
        if (WeapeonId.text != "")
        {
            playerInformation.WeapeonId = int.Parse(WeapeonId.text);
        }
        if (DamageMultiplier.text != "")
        {
            playerInformation.DamageMultiplier = float.Parse(DamageMultiplier.text);
        }
        if (AsMultiplier.text != "")
        {
            playerInformation.AsMultiplier = float.Parse(AsMultiplier.text);
        }
        if (HealthBonus.text != "")
        {
            playerInformation.HealthBonus = int.Parse(HealthBonus.text);
        }
        if (MsMultiplier.text != "")
        {
            playerInformation.MsMultiplier = float.Parse(MsMultiplier.text);
        }
        if (XpMultiplier.text != "")
        {
            playerInformation.XpMultiplier = float.Parse(XpMultiplier.text);
        }
        if (SizeMultiplier.text != "")
        {
            playerInformation.SizeMultiplier = float.Parse(SizeMultiplier.text);
        }
        if (ReviveBonus.text != "")
        {
            playerInformation.ReviveBonus = int.Parse(ReviveBonus.text);
        }
        if (ProjectileBonus.text != "")
        {
            playerInformation.ProjectileBonus = int.Parse(ProjectileBonus.text);
        }
        if (SummonDamageMultiplier.text != "")
        {
            playerInformation.SummonDamageMultiplier = float.Parse(SummonDamageMultiplier.text);
        }
        */
    }

    public void SwitchWeapeon(int Id)
    {
        playerInformation.WeapeonId = Id;
    }

    public void AdvanceToLevelSelection()
    {
        SaveToPlayerInfo();
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
        SaveToPlayerInfo();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void EnterShop()
    {
        SceneManager.LoadScene(6);
    }

    public void EnterAchievments()
    {
        SceneManager.LoadScene(7);
    }
}
