using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


public class levelingSystem : MonoBehaviour
{
    public static levelingSystem instance;

    public int level = 1;
    public int currentXp;
    public int xpNeeded;
    public int xpInccrease;
    public float xpFlatInccrease;
    public float FlatXpPlus;
    public float xpInccreaseMultiplier = 1;
    public GameObject levelUpMenu;
    public upgradeSorting sorting;
    public int totalXP;

    public XPbar bar;

    Health[] healths;
    public float healthPerLevel;

    public int red;
    public int green;
    public int blue;
    public int purple;
    public int yellow;

    [SerializeField] bool cheating = false;

    public bool Double = false;
    public bool FinallForm = false;

    [SerializeField] List<GameObject> DissableObj = new List<GameObject>();
    [SerializeField] Volume Volume;
    [SerializeField] Canvas Canvas;

    [SerializeField] List<Image> BoxImages = new List<Image>();
    [SerializeField] List<Image> BorderImages = new List<Image>();

    [SerializeField] GameObject SaveScreen;
    [SerializeField] GameObject UIVolume;

    [SerializeField] string FileName;


    private void Awake()
    {
        red = 0;
        green = 0;
        blue = 0;
        purple = 0;
        yellow = 0;

        totalXP = 0;
        instance = this;
        levelUpMenu.SetActive(false);
        bar.displayedLevel(level);
        bar.setMaxXp(xpNeeded);
        bar.setXP(currentXp);
    }

    private void Start()
    {
        PrintXpToFile();
    }

    private void Update()
    {
        if(Constants.Cheating)
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                addXp(xpNeeded - currentXp);
            }

            if(Input.GetKeyDown(KeyCode.J))
            {
                NonLevelUpgradeSorting Sorting = GameObject.FindWithTag("IronUpgrades").GetComponent<NonLevelUpgradeSorting>();
                SetUpLevelMenu(true,Sorting);
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                NonLevelUpgradeSorting Sorting = GameObject.FindWithTag("CorruptedUpgrades").GetComponent<NonLevelUpgradeSorting>();
                SetUpLevelMenu(true, Sorting);
            }

            if(Input.GetKeyDown(KeyCode.M))
            {
                if(SaveScreen.activeInHierarchy)
                {
                    SaveScreen.SetActive(false);
                    Time.timeScale = 1;
                }else
                {
                    SaveScreen.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }  
    }

    public void OpenLevelMenu()
    {
        UIVolume.SetActive(true);
        Volume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField);
        levelUpMenu.SetActive(true);
        CursorManager.instance.setCursorPointer();
        Time.timeScale = 0;
        depthOfField.active = true;
    }

    public void CloseLevelMenu()
    {
        UIVolume.SetActive(false);

        Volume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField);
        levelUpMenu.SetActive(false);
        CursorManager.instance.setCursorCrosshair();
        Time.timeScale = 1;
        depthOfField.active = false;
    }

    public void SetUpLevelMenu(bool Activate)
    {
        Volume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField);

        if (Activate)
        {
            sorting.setUpCards();
            OpenLevelMenu();

            for (int i = 0; i < DissableObj.Count; i++)
            {
                DissableObj[i].SetActive(false);
            }
        }
        else
        {
            CloseLevelMenu();
            //Canvas.renderMode = RenderMode.ScreenSpaceCamera;
            CheckBoxes();

            for (int i = 0; i < DissableObj.Count; i++)
            {
                DissableObj[i].SetActive(true);
            }
        }
    }

    public void SetUpLevelMenu(bool Activate,NonLevelUpgradeSorting Sorting)
    {
        Volume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField);

        if (Activate)
        {
            Sorting.setUpCards();
            OpenLevelMenu();
            //Canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            for (int i = 0; i < DissableObj.Count; i++)
            {
                DissableObj[i].SetActive(false);
            }
        }
        else
        {
            CloseLevelMenu();
            //Canvas.renderMode = RenderMode.ScreenSpaceCamera;
            CheckBoxes();

            for (int i = 0; i < DissableObj.Count; i++)
            {
                DissableObj[i].SetActive(true);
            }
        }
    }

    public void SetUpLevelMenuSpecial()
    {
        Volume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField);
        int Level = level;
        level = 10;

        sorting.setUpCards();
        OpenLevelMenu();

        for (int i = 0; i < DissableObj.Count; i++)
        {
            DissableObj[i].SetActive(false);
        }
        level = Level;
    }

    public void SetUpLevelMenuNormal()
    {
        Volume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField);
        int Level = level;
        level = 3;

        sorting.setUpCards();
        OpenLevelMenu();
        //Canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        for (int i = 0; i < DissableObj.Count; i++)
        {
            DissableObj[i].SetActive(false);
        }

        level = Level;
    }

    public void ScaleXp()
    {
        if (level % 10 != 0)
        {
            float pom = xpInccrease * xpInccreaseMultiplier;
            pom = pom + xpFlatInccrease;
            xpInccrease = (int)pom;
            xpNeeded += xpInccrease;
            //Debug.Log(xpNeeded);
            //Debug.Log(xpInccrease);
        }
        else
        {
            float pom = (xpInccreaseMultiplier - 1f) * 2.25f;
            pom += 1f;
            pom = pom * xpInccrease;
            pom = pom + (xpFlatInccrease * 2);
            xpInccrease = (int)pom;
            xpNeeded += xpInccrease;
            // Debug.Log(xpNeeded);
            //Debug.Log(xpInccrease);
        }
        xpFlatInccrease += FlatXpPlus;
    }

    public void PrintXpToFile()
    {
        int LevelTemp = 1;
        int XpNeededTemp = xpNeeded;
        int XpIncreaseTemp = xpInccrease;
        float XpFlatIncreaseTemp = xpFlatInccrease;
        List<string> StringList = new List<string>();

        for (int i = 0;i < 60;i++)
        {
            if (LevelTemp % 10 != 0)
            {
                float pom = XpIncreaseTemp * xpInccreaseMultiplier;
                pom = pom + XpFlatIncreaseTemp;
                XpIncreaseTemp = (int)pom;
                XpNeededTemp += XpIncreaseTemp;
            }
            else
            {
                float pom = (xpInccreaseMultiplier - 1f) * 2.25f;
                pom += 1f;
                pom = pom * XpIncreaseTemp;
                pom = pom + (XpFlatIncreaseTemp * 2);
                XpIncreaseTemp = (int)pom;
                XpNeededTemp += XpIncreaseTemp;
            }
            XpFlatIncreaseTemp += FlatXpPlus;

            StringList.Add(XpNeededTemp.ToString());
            LevelTemp++;
        }

        SaveManager.SaveLog(FileName,StringList);
    }

    public void IncreaseLevel()
    {
        level++;
        bar.displayedLevel(level);
        ScaleByLevel("Enemy");
        ScaleByLevel("Enviroment");
    }

    public void addXp(int xp)
    {
        currentXp += xp;
        totalXP += xp;
        bar.setXP(currentXp);

        if (currentXp >= xpNeeded)
        {
            currentXp -= xpNeeded;
            IncreaseLevel();
            ScaleXp();

            SetUpLevelMenu(true); 
        }
    }

    public void levelUp()
    {
        //Debug.Log("Level up");
        bar.displayedLevel(level);
        bar.setMaxXp(xpNeeded);
        bar.setXP(currentXp);
        SetUpLevelMenu(false);

        if (currentXp >= xpNeeded)
        {
            currentXp -= xpNeeded;
            ScaleXp();
            level++;
            ScaleByLevel("Enemy");
            ScaleByLevel("Enviroment");
            SetUpLevelMenu(true);
        }
    }

    public void CheckBoxes()
    {
        List<Color32> colors = new List<Color32>();
        bool Red = true;
        bool Blue = true;
        bool Purple = true;
        bool Yellow = true;
        bool Green = true;

        if(red == 0)
        {
            Red = false;
        }

        if(green == 0)
        {
            Green = false;
        }

        if(blue == 0)
        {
            Blue = false;
        }

        if(yellow == 0)
        {
            Yellow = false;
        }

        if(purple == 0)
        {
            Purple = false;
        }

        for (int i = 0; i < BoxImages.Count; i++)
        {
            if (BoxImages[i].color != new Color32(0, 0, 0, 0) )
            {
                if (BoxImages[i].color == new Color32(0, 255, 45, 255))
                {
                    Green = false;
                }else if (BoxImages[i].color == new Color32(255, 20, 0, 255))
                {
                    Red = false;
                }else if (BoxImages[i].color == new Color32(140, 0, 255, 255))
                {
                    Purple = false;
                }else if (BoxImages[i].color == new Color32(255, 255, 0, 255))
                {
                    Yellow = false;
                }else if (BoxImages[i].color == new Color32(65, 255, 255, 255))
                {
                    Blue = false;
                }

            }
        }

        for (int i = 0; i < BoxImages.Count; i++)
        {
            if (BoxImages[i].color == new Color32(0, 0, 0, 0))
            {
                if(Red)
                {
                    Red = false;
                    BoxImages[i].color = new Color32(255, 20, 0, 255);
                    BorderImages[i].color = new Color32(255, 20, 0, 160);
                }
                else if(Green)
                {
                    BoxImages[i].color = new Color32(0, 255, 45, 255);
                    BorderImages[i].color = new Color32(0, 255, 45, 160);
                    Green = false; 
                }else if(Blue)
                {
                    BoxImages[i].color = new Color32(65, 255, 255, 255);
                    BorderImages[i].color = new Color32(65, 255, 255, 160);
                    Blue = false;
                }else if(Yellow)
                {
                    BoxImages[i].color = new Color32(255, 255, 0, 255);
                    BorderImages[i].color = new Color32(255, 255, 0, 160);
                    Yellow = false;
                }else if(Purple)
                {
                    BoxImages[i].color = new Color32(140, 0, 255, 255);
                    BorderImages[i].color = new Color32(140, 0, 255, 160);
                    Purple = false;
                }
            }
        }
    }

    public void ScaleByLevel(string Tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(Tag);

        healths = new Health[gameObjects.Length];
        //Debug.Log(tag + " " + healths.Length);

        for (int i = 0; i < healths.Length; i++)
        {
            healths[i] = gameObjects[i].GetComponent<Health>();
            if (healths[i] != null)
            {
                if (healths[i].levelScaling)
                {
                    float diff = healths[i].maxHealth;
                    healths[i].maxHealth = ScaleHpByLevel(healths[i].baseMaxHealth);
                    
                    diff = healths[i].maxHealth - diff;
                    healths[i].health += (int)diff;
                    if (healths[i].healthBar != null)
                    {
                        healths[i].healthBar.SetMaxHealth(healths[i].maxHealth);
                        healths[i].healthBar.SetHealth(healths[i].health);
                    }   
                }
            }
        }
    }

    public float CalculateXpMultiplier()
    {
        float multiplier = (float)PlayerStats.sharedInstance.EXPmultiplier / 100f;
        multiplier = multiplier * MasterManager.Instance.PlayerInformation.XpMultiplier;
        return multiplier;
    }

    public int ScaleHpByLevel(int health)
    {
        float pom = health * healthPerLevel * (level - 1);
        pom = pom + health;
        pom = pom * MasterManager.Instance.PlayerInformation.MapEnemyHealthMultiplier;
        return (int)pom;
    }

}
