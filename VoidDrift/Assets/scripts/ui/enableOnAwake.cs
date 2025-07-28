using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableOnAwake : MonoBehaviour
{
    [SerializeField] GameObject miniMap;
    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject XpBar;
    [SerializeField] GameObject Timer;

    private void Awake()
    {
        miniMap.SetActive(true);
        HealthBar.SetActive(true);
        XpBar.SetActive(true);
        Timer.SetActive(true);
    }




}
