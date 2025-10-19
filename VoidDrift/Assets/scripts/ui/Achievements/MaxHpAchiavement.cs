using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHpAchiavement : Achiavement
{
    public float BonusHealth;

    public override void function(bool Win)
    {
        plaerHealth health = GameObject.FindWithTag("Player").GetComponent<plaerHealth>();
        if (health != null)
        {
            Debug.Log(health.maxHealth);
            Debug.Log(health.baseHealth * BonusHealth);
            if(health.maxHealth >= health.baseHealth * BonusHealth)
            {
                Unlock();
                PrizeActivation();
            }else
            {
                float Temp = health.maxHealth / health.baseHealth;
                Temp = Temp - 1;
                Temp = Temp * 100f;
                if (Temp > Current)
                {
                    Current = (int)Temp;
                }
            }
        
        }
        
    }

    public override void PrizeActivation()
    {
        if (AchiavementManager.instance.progressionState.UnlockedRunes.Contains(RuneConst.OrbDropRate) == false)
        {
            AchiavementManager.instance.progressionState.UnlockedRunes.Add(RuneConst.OrbDropRate);
        }
    }
}
