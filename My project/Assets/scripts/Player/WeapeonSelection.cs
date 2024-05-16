using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapeonSelection : MonoBehaviour
{
    [SerializeField] PlayerInformation playerInformation;

    private void Awake()
    {
        foreach (Transform Weapeon in transform)
        {
            weapeon WeapeonTemp = Weapeon.GetComponent<weapeon>();
            if (WeapeonTemp == null) continue;

            if(WeapeonTemp.Id == playerInformation.WeapeonId)
            {
                WeapeonTemp.gameObject.SetActive(true);
            }else WeapeonTemp.gameObject.SetActive(false);  
        }
    }
}
