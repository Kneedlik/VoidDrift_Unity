using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class OutOfCombatMovementSystem : MonoBehaviour
{
    PlayerActions MyInput;
    public static OutOfCombatMovementSystem instance;
    float timeStamp;
    public float coolDown;
    public bool active;
    public int speedAmount;
    public int DamageAmount;

    public GameObject Line;

    private void OnDisable()
    {
        MyInput.Disable();
    }

    void Awake()
    {
        instance = this;
        enabled = false;
        MyInput = new PlayerActions();
    }

    private void OnEnable()
    {
        MyInput.Enable();
        active = false;
        increaseMovespeed();
    }


    void Update()
    {
        if(MyInput.Gameplay.Fire.ReadValue<float>() > 0)
        {
            reduceMoveSpeed();
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

    public void reduceMoveSpeed()
    {
        if(active)
        {
            timeStamp = coolDown;
            active = false;
            PlayerStats.sharedInstance.IncreaseSpeed(speedAmount * -1);
            PlayerStats.sharedInstance.increaseDMG(DamageAmount * -1);
            Line.SetActive(false);
            PlayerStats.sharedInstance.UpdateStats();
        }   

    }

    public void increaseMovespeed()
    {
        if(active == false)
        {
            active = true;
            PlayerStats.sharedInstance.IncreaseSpeed(speedAmount);
            PlayerStats.sharedInstance.increaseDMG(DamageAmount);
            Line.SetActive(true);
            PlayerStats.sharedInstance.UpdateStats();
        }
  
    }

    public void reset()
    {
        if(active)
        {
            PlayerStats.sharedInstance.IncreaseSpeed(speedAmount * -1);
            PlayerStats.sharedInstance.increaseDMG(DamageAmount * -1);
            active = false;
            Line.SetActive(false);
        }
        
    }

}
