using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenSlayerSystem : MonoBehaviour
{
    public int damage;
    int bulletCount;
    public int delay = 3;
    public GameObject icon;
    public float duration;

    public bool increaseDamage = false;
    public int increaseAmount;
    public float max;

    

    public static KrakenSlayerSystem sharedInstance;
    void Start()
    {
        bulletCount = 0;
        sharedInstance = this;
        icon.SetActive(false);
    }

    public void krakenProc(GameObject target)
    {
        bulletCount++;

        if(bulletCount >= delay)
        {
            StartCoroutine(flashIcon());
           weapeon gun = target.GetComponent<weapeon>();
            gun.extraDamage += damage;

            bulletCount = 0;
        }
    }

    IEnumerator flashIcon()
    {
        icon.SetActive(true);
        yield return new WaitForSeconds(duration);
        icon.SetActive(false);
    }

    
}
