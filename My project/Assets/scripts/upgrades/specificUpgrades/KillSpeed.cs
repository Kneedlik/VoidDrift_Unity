using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSpeed : upgrade
{
    [SerializeField] int increaseAmount;
    [SerializeField] int totalAmount;
    [SerializeField] float durationAmount;
    [SerializeField] float totalDuration;

    bool active;

    void Start()
    {
        Type = type.purple;
        setColor();
        active = false;
    }

    public override void function()
    {
        totalAmount += increaseAmount;

        if(level == 0)
        {
            eventManager.OnKill += startBurst;
            totalAmount = 25;
            description = string.Format("After killing an enemy gain + {0}% movement speed",increaseAmount);

        }else if(level == 1)
        {
            totalAmount += increaseAmount;
            description = string.Format("Burst duration + {0} seconds Movement speed on kill + 10%",durationAmount);
        }else if(level == 2)
        {
            totalDuration += durationAmount;
            totalAmount += 10;
            description = string.Format("After killing an enemy gain + {0}% movement speed", increaseAmount);
        }else if(level == 3)
        {
            totalAmount += increaseAmount;
            description = string.Format("Burst duration + {0} seconds Movement speed on kill + 10%",durationAmount);
        }else if (level == 4)
        {
            totalAmount += 10;
            totalDuration += durationAmount;
        }

        level++;
    }

    public void startBurst(GameObject target)
    {
        if(target.tag.Contains("Enemy"))
        {
            if (active == false)
            {
                active = true;
                StartCoroutine(speedBurst());
            }
        }
        

    }

    IEnumerator speedBurst()
    {
        int pom = totalAmount;
        PlayerStats.sharedInstance.IncreaseSpeed(totalAmount);
        yield return new WaitForSeconds(totalDuration);
        PlayerStats.sharedInstance.IncreaseSpeed(pom * -1);
        active = false;
    }



    

}
