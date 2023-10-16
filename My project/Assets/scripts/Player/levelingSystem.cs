using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }

        
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
            sorting.setUpCards();
            levelUpMenu.SetActive(true);
            CursorManager.instance.setCursorPointer();
            Time.timeScale = 0;
            
        }
    }

    public void levelUp()
    {
        bar.displayedLevel(level);
        bar.setMaxXp(xpNeeded);
        bar.setXP(currentXp);
        levelUpMenu.SetActive(false);
        CursorManager.instance.setCursorCrosshair();
        Time.timeScale = 1;

        if (currentXp >= xpNeeded)
        {
            level++;
            ScaleByLevel();
            sorting.setUpCards();
            levelUpMenu.SetActive(true);
            CursorManager.instance.setCursorPointer();
            Time.timeScale = 0;
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
