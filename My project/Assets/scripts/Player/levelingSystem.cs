using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    private void Update()
    {
        if(cheating)
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
        }  
    }

    public void SetUpLevelMenu(bool Activate)
    {
        Volume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField);

        if (Activate)
        {
            sorting.setUpCards();
            levelUpMenu.SetActive(true);
            CursorManager.instance.setCursorPointer();
            
            Time.timeScale = 0;
            depthOfField.active = true;
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            for (int i = 0; i < DissableObj.Count; i++)
            {
                DissableObj[i].SetActive(false);
            }
        }
        else
        {
            levelUpMenu.SetActive(false);
            CursorManager.instance.setCursorCrosshair();
            Time.timeScale = 1;
            depthOfField.active = false;
            Canvas.renderMode = RenderMode.ScreenSpaceCamera;
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
            levelUpMenu.SetActive(true);
            CursorManager.instance.setCursorPointer();

            Time.timeScale = 0;
            depthOfField.active = true;
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            for (int i = 0; i < DissableObj.Count; i++)
            {
                DissableObj[i].SetActive(false);
            }
        }
        else
        {
            levelUpMenu.SetActive(false);
            CursorManager.instance.setCursorCrosshair();
            Time.timeScale = 1;
            depthOfField.active = false;
            Canvas.renderMode = RenderMode.ScreenSpaceCamera;
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
        levelUpMenu.SetActive(true);
        CursorManager.instance.setCursorPointer();

        Time.timeScale = 0;
        depthOfField.active = true;
        Canvas.renderMode = RenderMode.ScreenSpaceOverlay;

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
        levelUpMenu.SetActive(true);
        CursorManager.instance.setCursorPointer();

        Time.timeScale = 0;
        depthOfField.active = true;
        Canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        for (int i = 0; i < DissableObj.Count; i++)
        {
            DissableObj[i].SetActive(false);
        }

        level = Level;
    }

    public void addXp(int xp)
    {
        currentXp += xp;
        totalXP += xp;
        bar.setXP(currentXp);

        if (currentXp >= xpNeeded)
        {
            currentXp -= xpNeeded;
            level++;

            if (level % 10 != 0)
            {
                float pom = xpInccrease * xpInccreaseMultiplier;
                xpInccrease = (int)pom;
                xpNeeded += xpInccrease;
                //Debug.Log(xpNeeded);
                //Debug.Log(xpInccrease);
            }
            else
            {
                float pom = (xpInccreaseMultiplier - 1f) * 4f;
                pom += 1f;
                pom = pom * xpInccrease;
                xpInccrease = (int)pom;
                xpNeeded += xpInccrease;
               // Debug.Log(xpNeeded);
                //Debug.Log(xpInccrease);
            }

            ScaleByLevel();
            bar.displayedLevel(level);
            SetUpLevelMenu(true); 
        }
    }

    public void levelUp()
    {
        bar.displayedLevel(level);
        bar.setMaxXp(xpNeeded);
        bar.setXP(currentXp);
        SetUpLevelMenu(false);

        if (currentXp >= xpNeeded)
        {
            level++;
            ScaleByLevel();
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

    public void ScaleByLevel()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

        healths = new Health[gameObjects.Length];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            healths[i] = gameObjects[i].GetComponent<Health>();
            if (healths[i] != null)
            {
                if (healths[i].levelScaling)
                {
                    float diff = healths[i].maxHealth;
                    float pom = healths[i].baseMaxHealth * healthPerLevel * (level - 1);
                    
                    healths[i].maxHealth = healths[i].baseMaxHealth + (int)pom;
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
}
