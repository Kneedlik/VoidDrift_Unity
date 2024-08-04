using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusatAI : MonoBehaviour
{
    float TimeStamp;
    bool Attacking;
    int OnCoolDown;


    void Start()
    {
        OnCoolDown = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if (TimeStamp <= 0 && Attacking == false)
        {
            Attacking = true;
            DecideAttack();
        }
    }

    void DecideAttack()
    {
        int Rand;
        switch(OnCoolDown)
        {
            case 1:
                Rand = Random.Range(1, 3);
                if(Rand == 1)
                {
                    OnCoolDown = 2;
                    StartCoroutine(Attack2());
                }
                else if (Rand == 2)
                {
                    OnCoolDown = 3;
                    StartCoroutine(Attack3());
                }  
                break;
            case 2:
                Rand = Random.Range(1, 3);
                if (Rand == 1)
                {
                    OnCoolDown = 1;
                    StartCoroutine(Attack1());
                }
                else if (Rand == 2)
                {
                    OnCoolDown = 2;
                    StartCoroutine(Attack3());
                }
                break;
            case 3:
                Rand = Random.Range(1, 3);
                if (Rand == 1)
                {
                    OnCoolDown = 1;
                    StartCoroutine(Attack1());
                }
                else if (Rand == 2)
                {
                    OnCoolDown = 2;
                    StartCoroutine(Attack2());
                }
                break;
        }
    }

    IEnumerator Attack1()
    {
        yield return null;


    }

    IEnumerator Attack2()
    {
        yield return null;
    
    }

    IEnumerator Attack3()
    {
        yield return null;
    }
}
