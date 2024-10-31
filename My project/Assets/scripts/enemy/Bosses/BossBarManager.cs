using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossBarManager : MonoBehaviour
{
    public static BossBarManager Instance;
    public List<Health> health;
    [SerializeField] List<GameObject> healthG;
    [SerializeField] List<TextMeshProUGUI> textMeshProUGUI;
    [SerializeField] List<float> TimeStamps;
    [SerializeField] float RemoveTime;

    void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        for (int i = 0; i < TimeStamps.Count; i++)
        {
            if (TimeStamps[i] > 0)
            {
                TimeStamps[i] -= Time.deltaTime;
            }else if (healthG[i].activeSelf && health[i] != null)
            {
                RemoveBar(health[i]);
            }
        }
    }

    public void AddBar(Health healthT)
    {
        Slider slider;
        if(healthT == null || health.Contains(healthT))
        {
            return;
        }
        
        for(int i = 0; i < health.Count; i++)
        {
            if (health[i] == null)
            {
                health[i] = healthT;
                healthG[i].SetActive(true);
                TimeStamps[i] = RemoveTime;
                slider = healthG[i].GetComponent<Slider>();
                slider.maxValue = healthT.maxHealth;
                slider.value = healthT.health;

                textMeshProUGUI[i].text = string.Format("{0} / {1}", health[i].health, health[i].maxHealth);
                return;
            }
        }    
    }

    public void RemoveBar(Health healthT)
    {
        if(health.Contains(healthT) == false)
        {
            return;
        }

        for(int i = 0;i < health.Count;i++)
        {
            if (health[i] == healthT)
            {
                health[i] = null;
                healthG[i].SetActive(false);
            }
        }

        Sort();
    }

    public void UpdateBar()
    {
        Slider slider;
        for(int i = 0;i < health.Count ; i++)
        {
            if (health[i] != null)
            {
                slider = healthG[i].GetComponent<Slider>();
                if (health[i].health > slider.value || health[i].health < slider.value || health[i].maxHealth > slider.maxValue || health[i].maxHealth < slider.maxValue)
                {
                    slider.value = health[i].health;
                    slider.maxValue = health[i].maxHealth;
                    textMeshProUGUI[i].text = string.Format("{0} / {1}", health[i].health, health[i].maxHealth);
                    TimeStamps[i] = RemoveTime;
                }
            }
        }
    }

    public void Sort()
    {
        Slider slider;
        if (health[2] != null && health[1] == null) 
        {
            health[1] = health[2];
            health[2] = null;
            healthG[2].SetActive(false);
            healthG[1].SetActive(true);

            slider = healthG[1].GetComponent<Slider>();
            slider.value = health[1].health;
            slider.maxValue = health[1].maxHealth;

            textMeshProUGUI[1].text = string.Format("{0} / {1}", health[1].health, health[1].maxHealth);
        }

        if (health[1] != null && health[0] == null)
        {
            health[0] = health[1];
            health[1] = null;
            healthG[1].SetActive(false);
            healthG[0].SetActive(true);

            slider = healthG[0].GetComponent<Slider>();
            slider.value = health[0].health;
            slider.maxValue = health[0].maxHealth;

            textMeshProUGUI[0].text = string.Format("{0} / {1}", health[0].health, health[0].maxHealth);
        }
    }
}
