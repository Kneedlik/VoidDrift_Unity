using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public ProgressionState Progress;
    public PlayerInformation PlayerInfo;
    public static ShopManager Instance;
    public List<UpgradeBox> upgradeBoxes = new List<UpgradeBox>();

    //Window
    public Image WindowIcon;
    public TMP_Text Description;
    public TMP_Text Name;
    public TMP_Text IncreaseText;
    public GameObject WindowObj;


    // Start is called before the first frame update
    private void Awake()
    {
        HideWindow();
        Instance = this;
        ResetAll();
    }

    public void ResetAll()
    {
        Progress.ShopUpgradesProgression.Clear();
        for (int i = 0; i < upgradeBoxes.Count; i++)
        {
            upgradeBoxes[i].LoadFromProgression();
        }
    }

    public void SetWindowDescription(int Id)
    {
        switch (Id) 
        {
            case UpgradeConsts.Damage: Description.text = string.Format("Damage from all sources");
                break;
            case UpgradeConsts.Health: Description.text = string.Format("Bunus maximum health");
                break;
            case UpgradeConsts.AttackSpeed: Description.text = string.Format("Adds attack speed instease to players weapeon");
                break;
            case UpgradeConsts.Speed: Description.text = string.Format("Adds movement speed increase");
                break;
            case UpgradeConsts.Area: Description.text = string.Format("Area size of all projectiles and explosions");
                break;
            case UpgradeConsts.Xp: Description.text = string.Format("Experience gain form all sources");
                break;
            case UpgradeConsts.Projectiles: Description.text = string.Format("Number of extra projectiles of players weapeon");
                break;
            case UpgradeConsts.SummonDamage: Description.text = string.Format("Damage dealt by summons");
                break;
            case UpgradeConsts.Revives: Description.text = string.Format("Number of extra lives");
                break;
            case UpgradeConsts.Rerols: Description.text = string.Format("Chances to reroll upgrades during game");
                break;
            case UpgradeConsts.HealthRegen: Description.text = string.Format("Health regeneration per second");
                break;
            case UpgradeConsts.GoldGain: Description.text = string.Format("Gold gain from all sources");
                break;
        }
    }

    public void HideWindow()
    {
        WindowObj.SetActive(false);
    }

    public void ExitToPlayerMenu()
    {
        SceneManager.LoadScene(1);
    }


}
