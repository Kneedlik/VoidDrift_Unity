using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class levelDisplay : MonoBehaviour
{
    public GameObject player;
    levelingSystem levelingSystem;
    TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        levelingSystem = player.GetComponent<levelingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void inccreaseLevel(int level)
    {
        //  textMeshProUGUI.text = level.ToString();
        textMeshProUGUI.text = string.Format("Level %d",level);
    }
}
