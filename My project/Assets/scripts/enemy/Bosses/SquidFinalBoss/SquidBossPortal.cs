using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SquidBossPortal : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] GameObject ProjectilePrefab;
    [SerializeField] float CoolDown0;
    [SerializeField] float CoolDown1;
    [SerializeField] int EnemyAmount;
    [SerializeField] int ProjectileAmount;
    [SerializeField] float DestroyTime;
    [SerializeField] float DestroyDelay;
    [SerializeField] float OffsetMax;
    [SerializeField] float OffsetMin;
    //[SerializeField] int damageA2;

    int state;
    float timeStamp;
    
    bool finished;
    bool ready;

    int MaxAmount;
    int CurrentIndex;

    SpriteRenderer SpriteColor;

    [SerializeField] float ColorDecaySpeed;
    float alpha;
    float alphaTemp;

    [SerializeField] float SizeDecaySpeed;
    float size;
    float sizeTemp;

    Transform Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        SpriteColor = GetComponent<SpriteRenderer>();
        alpha = SpriteColor.color.a;
        alphaTemp = 0;
        size = transform.localScale.x;
        sizeTemp = 0;

        SizeDecaySpeed *= transform.localScale.x;
        CurrentIndex = 0;
        finished = false;
        state = Random.Range(0, 2);
        Debug.Log(state);

        transform.localScale = new Vector3(0,0,1);
        SpriteColor.color = new Color(SpriteColor.color.r, SpriteColor.color.g, SpriteColor.color.b, 0);

        if (state == 0)
        {
            MaxAmount = EnemyAmount;
        }else if(state == 1)
        {
            MaxAmount += ProjectileAmount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (ready)
        {
            if (timeStamp <= 0 && finished == false)
            {
                if (state == 0)
                {

                    GameObject T = Instantiate(EnemyPrefab, transform.position, Quaternion.Euler(0, 0, 0));
                    KnedlikLib.lookAt2d(T.transform, Player, 0);
                    timeStamp = CoolDown0;
                }
                else if (state == 1)
                {
                    int Rand = Random.Range(0, 2);
                    float RandOffset = Random.Range(OffsetMin, OffsetMax);

                    if (Rand == 1)
                    {
                        RandOffset *= -1;
                    }

                    timeStamp = CoolDown1;
                    GameObject T = Instantiate(ProjectilePrefab, transform.position, Quaternion.Euler(0, 0, 0));
                    KnedlikLib.lookAt2d(T.transform, Player, 270 + RandOffset);
                    //T.transform.rotation = Quaternion.Euler(0, 0, T.transform.rotation.eulerAngles.z + RandOffset);
                    //Debug.Log(T.transform.rotation.eulerAngles.z);
                }


                if (KnedlikLib.IncreaseIndex(ref CurrentIndex, MaxAmount) == false)
                {
                    finished = true;
                    timeStamp = DestroyDelay;
                    Destroy(gameObject, DestroyTime);
                }
            }
        }else
        {
            if (alphaTemp < alpha)
            {
                alphaTemp += ColorDecaySpeed * Time.deltaTime;
            }
            else alphaTemp = 255;
            SpriteColor.color = new Color(SpriteColor.color.r, SpriteColor.color.g, SpriteColor.color.b, alphaTemp);

            if (sizeTemp < size)
            {
                sizeTemp += SizeDecaySpeed * Time.deltaTime;
            }
            else size = 1;
            transform.localScale = new Vector3(sizeTemp, sizeTemp, sizeTemp);

            if (sizeTemp >= size && alphaTemp >= alpha)
            {
                ready = true;
            }

        }

        if (finished && timeStamp <= 0)
        {
            if (alpha > 0)
            {
                alpha -= ColorDecaySpeed * Time.deltaTime;
            }else alpha = 0;

            SpriteColor.color = new Color(SpriteColor.color.r, SpriteColor.color.g, SpriteColor.color.b, alpha);

            if(size > 0)
            {
                size -= SizeDecaySpeed * Time.deltaTime;
            }else size = 0;

            transform.localScale = new Vector3(size,size,size); 
            
        }

        
    }
}
