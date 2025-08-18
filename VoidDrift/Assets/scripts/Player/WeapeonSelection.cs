using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapeonSelection : MonoBehaviour
{
    MasterManager Master;

    public void ActivateWeapeon()
    {
        Master = GameObject.FindWithTag("Master").GetComponent<MasterManager>();

        foreach (Transform Weapeon in transform)
        {
            weapeon WeapeonTemp = Weapeon.GetComponent<weapeon>();
            if (WeapeonTemp == null) continue;

            if (WeapeonTemp.Id == Master.PlayerInformation.WeapeonId)
            {
                WeapeonTemp.gameObject.SetActive(true);
            }
            else WeapeonTemp.gameObject.SetActive(false);
        }
    }
}
