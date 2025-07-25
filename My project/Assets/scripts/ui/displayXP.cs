using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayXP : MonoBehaviour
{
   public TextMeshProUGUI textMeshProUGUI;

  //  private void Start()
   // {
   //     textMeshProUGUI = GetComponent<TextMeshProUGUI>();
   // }

    private void OnEnable()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = string.Format("{0}", levelingSystem.instance.totalXP);
    }

   // private void Update()
   // {
   //     textMeshProUGUI.text = string.Format("{0}", levelingSystem.instance.totalXP);
   // }
}
