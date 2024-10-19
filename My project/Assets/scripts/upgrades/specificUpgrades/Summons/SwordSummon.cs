using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SwordSummon : Summon
{
    bool ready;
    float timeStamp;

    bool Switch;
    [SerializeField] GameObject SlashObjectT;
    [SerializeField] GameObject SlashObjectF;
    public bool Crit = false;

    public int slashAmount = 1;
    public float slashDelay;
    public int TicksNeeded;
    int CurrentTick;
   // List<ShapeLessTarget>Targets = new List<ShapeLessTarget>();

 //  public class ShapeLessTarget
  // {
  //     public GameObject target;
  //     public int TiksStack;
  //     public int CurrentTick;
  // }

    void Start()
    {
        CurrentTick = 0;
        ScaleSummonStats();
        PlayerStats.OnLevel += ScaleSummonStats;

        eventManager.OnImpact += strike;
    }

    private void Update()
    {
        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (timeStamp <= 0)
        {
            ready = true;
        }
    }

    public void strike(GameObject target,int damage, ref int bonusDamage)
    {
        /*  bool Contains = false;
          Sort();
          for (int i = 0; i < Targets.Count; i++)
          {
              if (Targets[i].target == target)
              {
                  Contains = true;
                  break;
              }
          }

          if(Contains == false)
          {
              ShapeLessTarget Temp = new ShapeLessTarget();
              Temp.target = target;
              Temp.TiksStack = slashAmount;
              Temp.TiksStack = 0;

              Targets.Add(Temp);
              StartCoroutine(slash(target));
          }
        */
        if (ready)
        {
            StartCoroutine(slash(target));

            timeStamp = fireRate;
            ready = false;
        }
    }

    IEnumerator slash(GameObject target)
    {
        Health health = target.GetComponent<Health>();

        for (int i = 0; i < slashAmount; i++)
        {
            if (health != null)
            {
                int Damage = damage;

                if(Switch)
                {
                    Instantiate(SlashObjectT,target.transform.position,Quaternion.Euler(0,0,0));
                    Switch = false;
                }else
                {
                    Instantiate(SlashObjectT, target.transform.position, Quaternion.Euler(0, 0, 0));
                    Switch = false;
                }

                if (eventManager.SummonOnImpact != null)
                {
                    eventManager.SummonOnImpact(target, damage, ref Damage);
                }
            
                if(Crit)
                {
                    if(CurrentTick == TicksNeeded)
                    {
                        CurrentTick = 0;
                        float pom = (float)Damage * CritSystem.instance.critMultiplier;
                        Damage = (int)pom;
                    }else
                    {
                        CurrentTick++;
                    }
                }

                health.TakeDamage(Damage);
                yield return new WaitForSeconds(slashDelay);
            }
        }
    }

    public override int PrintPowerLevel()
    {
        float PowerLevel = 0;
        float TimeSpend = 0;

        PowerLevel += baseDamage;
        TimeSpend += baseFireRate;
        PowerLevel = PowerLevel * slashAmount;
        PowerLevel = PowerLevel / TimeSpend;
        Debug.Log(string.Format("Power level: {0}", (int)PowerLevel));
        return (int)PowerLevel;
    }

    void Sort()
    {
       // bool Temp;

       // do
       // {
        //    Temp = false;
        //    for (int i = 0; i < Targets.Count; i++)
       //     {
       //         if (Targets[i].target == null)
       //         {
       //             Temp = true;
       //             Targets.RemoveAt(i);
       //         }
       //     }
       // } while (Temp == true);   
    }

}
