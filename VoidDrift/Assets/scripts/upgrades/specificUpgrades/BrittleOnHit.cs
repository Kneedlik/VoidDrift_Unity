using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrittleOnHit : upgrade
{
    public static BrittleOnHit instance;

    public float amount;
    [SerializeField] float amount2;
    public float chance;
    public float duration;
    public float speedPenalty;

    void Start()
    {
        instance = this;
        Type = type.blue;
        setColor();
    }


    public override void function()
    {
        
        if (level == 0)
        {
            eventManager.OnImpact += BrittleSystem.Instance.ApplyBrittle;
            eventManager.SummonOnImpact += BrittleSystem.Instance.ApplyBrittle;

            description = string.Format("Chance to apply brittle + {0}% Brittle damage + {1}%", chance, amount * 100);
        } else if (level == 1)
        {
            BrittleSystem.Instance.armorPierce += amount;
            BrittleSystem.Instance.chance += chance;
            description = string.Format("Brittle speed decrease + {0}% Brittle duration + {1} seconds", speedPenalty * 100, duration);
        }
        else if (level == 2)
        {
            BrittleSystem.Instance.duration += duration;
            BrittleSystem.Instance.speedDecrease += speedPenalty;
            description = string.Format("Aplying brittle now causes explosion dealing damage to neary enemies");
        } else if (level == 3)
        {
            BrittleSystem.Instance.explode = true;
            description = string.Format("Chance to apply brittle + {0}% Brittle damage + {1}%", chance, amount * 100);
        } else if (level == 4)
        {
            BrittleSystem.Instance.armorPierce += amount;
            BrittleSystem.Instance.chance += chance;
            description = string.Format("Brittle now freezes enemies in place for 1 second making them unable to move");
        } else if (level == 5)
        {
            BrittleSystem.Instance.freeze = true;
            description = string.Format("Chance to apply brittle + {0}% Brittle damage + {1}%", chance, amount2 * 100);
        }else if(level == 6)
        {
            BrittleSystem.Instance.chance += chance;
            BrittleSystem.Instance.armorPierce += amount2;
            description = string.Format("Brittle speed decrease + 20% Freeze duration + 0.5 seconds");
        }else if(level == 7)
        {
            BrittleSystem.Instance.speedDecrease += 0.2f;
            BrittleSystem.Instance.freezeDuration += 0.5f;
            description = string.Format("Brittle damage + {0}%",amount2 * 100);
        }else if(level == 8)
        {
            BrittleSystem.Instance.armorPierce += amount2;
            description = string.Format("Brittle damage + {0}%", amount * 100);
            rarity -= 25;
        }

        else
        {  
            BrittleSystem.Instance.armorPierce += amount;
        }
        
        level++;
    }
}
