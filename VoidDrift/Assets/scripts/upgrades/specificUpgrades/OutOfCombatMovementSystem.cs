using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfCombatMovementSystem : MonoBehaviour
{
  //  public static OutOfCombatMovementSystem instance;
    float timeStamp;
    public float coolDown;
    public bool active;
    public int speedAmount;

    public GameObject line1;
    public GameObject line2;

    void Start()
    {
       // instance = this;
    }

    private void OnEnable()
    {
        active = false;
        increaseMovespeed();
    }


    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            reduceMoveSpeed(0);
        }

        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if(timeStamp <= 0 && active == false)
        {
           increaseMovespeed();
        }
    }

    public void reduceMoveSpeed(int amount)
    {
        if(active)
        {
            timeStamp = coolDown;
            active = false;
            PlayerStats.sharedInstance.IncreaseSpeed(speedAmount * -1);
            line1.SetActive(false);
            line2.SetActive(false);
        }   

    }

    public void increaseMovespeed()
    {
        if(active == false)
        {
            active = true;
            PlayerStats.sharedInstance.IncreaseSpeed(speedAmount);
            line1.SetActive(true);
            line2.SetActive(true);
        }
  
    }

    public void reset()
    {
        if(active)
        {
            PlayerStats.sharedInstance.IncreaseSpeed(speedAmount * -1);
            active = false;
            line1.SetActive(false);
            line2.SetActive(false);
        }
        
    }

}
