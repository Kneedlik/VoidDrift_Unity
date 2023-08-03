using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealSystem : MonoBehaviour
{
    public static LifeStealSystem instance;

    [SerializeField] float timeStamp;
    [SerializeField] float timeStamp2;
    
    public float coolDown;
    public float LeechDuration;
    plaerHealth PHealth;
   [SerializeField] bool active;

    public int Storage;


    void Start()
    {
        instance = this;
        PHealth = GameObject.FindWithTag("Player").GetComponent<plaerHealth>();
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if(timeStamp2 > 0)
        {
            timeStamp2 -= Time.deltaTime;
        }

        if(active == false)
        {
            if(timeStamp <= 0)
            {
                active = true;
                timeStamp2 = LeechDuration;
            }
        }
        
       if(active)
        {
            if(timeStamp2 <= 0)
            {
                active = false;
                timeStamp = coolDown;
            }
        }
    }

    public void Leech(int damage)
    {
        if(active)
        {
            Storage += damage;

            while(Storage > 150)
            {
                PHealth.increaseHP(1);
                Storage -= 150;
            }
        }
    }
}
