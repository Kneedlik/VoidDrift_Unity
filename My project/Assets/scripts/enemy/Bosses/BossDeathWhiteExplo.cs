using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathWhiteExplo : DeathFunc
{
    [SerializeField] List<Transform> ExploPos = new List<Transform>();
    [SerializeField] float ExploDelay;
    [SerializeField] GameObject ExploPrefab;
    [SerializeField] float AlphaDecaySpeed;
    [SerializeField] Material flashMaterial;
    [SerializeField] int Repeat;
    SpriteRenderer spriteRenderer;
    float AlphaTemp;
    //SpriteRenderer SpriteColor;


    private void Start()
    {
        Health health = GetComponent<Health>();

        //Debug.Log("Added");
        health.DeathFunc.Add(this);
       
        //Debug.Log(health.DeathFunc123.Count);


        health = GetComponent<Health>();
        health.DeathDelay = ExploDelay * (ExploPos.Count);
        health.DeathDelay = (health.DeathDelay * Repeat) + 2.5f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        //SpriteColor = GetComponent<SpriteRenderer>();
        Repeat += 1; 
    }

    public override void function()
    {
        spriteRenderer.material = flashMaterial;

        AlphaTemp = 0.8f; //spriteRenderer.color.a;
        if(AlphaDecaySpeed > 0)
        {
            StartCoroutine(FadeCorutine());
        }

        StartCoroutine(functionCorutine());
    }

    IEnumerator FadeCorutine()
    {
        while(KnedlikLib.AlphaFade(spriteRenderer, AlphaDecaySpeed,ref AlphaTemp))
        {
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator functionCorutine()
    {
        yield return new WaitForSeconds(ExploDelay);

        for (int j = 0; j < Repeat; j++)
        {
            for (int i = 0; i < ExploPos.Count; i++)
            {
                Instantiate(ExploPrefab, ExploPos[i].position, Quaternion.Euler(0, 0, 0));
                yield return new WaitForSeconds(ExploDelay);
            }
        }

        yield return null;
    }  
}
