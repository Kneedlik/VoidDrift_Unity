using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instanciateItems : MonoBehaviour
{
    public GameObject[] items;
    public Transform[] spots;



    private void OnDestroy()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Instantiate(items[i], spots[i]);
        }
    }

}
