using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerScaling : MonoBehaviour
{
    timer timer;
    void Start()
    {
        timer = GameObject.FindWithTag("Timer").GetComponent<timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
