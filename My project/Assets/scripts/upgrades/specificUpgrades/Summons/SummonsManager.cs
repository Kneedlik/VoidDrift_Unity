using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonsManager : MonoBehaviour
{
    public static SummonsManager instance;
    public Transform[] Summons;
                                          
    public int summonCount;
    public int maxSummons;

    private void Start()
    {
        instance = this;
        summonCount = 0;
    }

    public bool addSummon(GameObject summon,out GameObject Summon1)
    {

        for (int i = 0; i < Summons.Length; i++)
        {
            if (Summons[i].childCount == 0)
            {
                Summon1 =  Instantiate(summon, Summons[i].position,Quaternion.Euler(0,0,0));
                Summon1.transform.SetParent(Summons[i]);
                summonCount++;
   
                return true;
            }
        }

        if(summonCount > 1 && PlayerStats.sharedInstance.OneSummonBuff)
        {
            PlayerStats.sharedInstance.OneSummonBuff = false;
            PlayerStats.sharedInstance.SummonDamage -= 50;
        }

        Summon1 = summon;
        return false;
    }
}
