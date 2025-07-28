using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tenacity : MonoBehaviour
{
    public float tenacity = 1f;
    public bool Decrease = true;
    float MinTenacity = 0.05f;
    float TenacityDecrease = 0.035f;
   
    public void UpdateTenacity()
    {
        if(tenacity < 0)
        {
            tenacity = 0;
            return;
        }

        if (Decrease)
        {
            tenacity = tenacity - TenacityDecrease;
            if (tenacity < MinTenacity)
            {
                tenacity = MinTenacity;
            }
        }
    }

    public float CalculateForce(float Force)
    {
        UpdateTenacity();
        return tenacity * Force;
    }

    public float CalculateDuration(float Duration)
    {
        UpdateTenacity();
        return tenacity * Duration;

    }
    
}
