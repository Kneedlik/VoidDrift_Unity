using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableLight : MonoBehaviour
{

    public gunShipAI aI;
    public Transform self;
    private bool alert;


    // Update is called once per frame
    void Update()
    {
        alert = aI.getAlert();

        if(alert == false)
        {
            self.gameObject.SetActive(false);
        }else
        {
            self.gameObject.SetActive(true);
        }
    }
}
