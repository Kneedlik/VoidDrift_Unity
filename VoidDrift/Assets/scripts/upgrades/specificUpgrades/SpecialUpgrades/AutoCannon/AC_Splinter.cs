using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AC_Splinter : upgrade
{
    public int Chance;
    public int ChanceIncrease = 33;
    public int Damage;
    public float Force;
    [SerializeField] int Amount = 3;
    [SerializeField] GameObject Prefab;


    private void Start()
    {
        Type = type.special;
        setColor();
        int Temp = Chance + ChanceIncrease;
        description = string.Format("When a bullet is destroied it has %d chance to split into %d shrapnels dealing 33% bullets damage",Temp);
    }

    public override bool requirmentsMet()
    {
        if(levelingSystem.instance.level >= 10)
        {
            return true;
        }return false;
        
    }

    public override void function()
    {
        if (level == 0)
        {
            eventManager.OnImpact += SplinterFunc;
        }

        Chance += ChanceIncrease;

        int Temp = Chance + ChanceIncrease;
        description = string.Format("When a bullet is destroied it has %d chance to split into %d shrapnels dealing 33% bullets damage", Temp);

        level++;
    }

    public void SplinterFunc(GameObject target,int damage,ref int Damage)
    {
        int Rand = Random.Range(0,100);

        if (Rand <= Chance)
        {
            float Rand1 = Random.Range(0,90);
            float Angle = 360 / Amount;
            float Temp = Angle + Rand1;
            for (int i = 0; i < Amount; i++)
            {
                GameObject pom = Instantiate(Prefab,target.transform.position,Quaternion.Euler(0,0,Temp));
                Rigidbody2D rb = pom.GetComponent<Rigidbody2D>();
                rb.AddForce(pom.transform.up * Force,ForceMode2D.Impulse);
               
                Rand1 = Random.Range(80, 120);
                Rand1 = Rand1 / 10;
                KnedlikLib.ScaleParticleByFloat(pom, Rand1, true);

                Rand = Random.Range(0,360);
                pom.transform.rotation = Quaternion.Euler(0, 0, Rand);

                Temp += Angle;   
            }
        }
    }
}
