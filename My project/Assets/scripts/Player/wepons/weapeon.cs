using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapeon : MonoBehaviour
{
    public Transform firePoint;
    public Transform SidefirePoint1;
    public Transform SidefirePoint2;

    public float ASmultiplier = 1;

    public int damage;
    public int baseDamage;
    public int extraDamage = 0;
    public float BaseForce = 10f;
    public float ForceMultiplier = 1;
    public float Force;
    public float size;
    public float baseSize = 1;
    public float baseBulletCoolDown;
    public float bulletCoolDown;
    public int pierce = 1;
    public float knockBack;
    float pomDamage;
    public int projectileCount;
    public int sideProjectiles = 0;
    

    public void updateDamage(int multiplier)
    {
        float realMultiplier = (float)multiplier / 100f;
        pomDamage = (float)baseDamage * realMultiplier;
        damage = (int)pomDamage;
    }

    public void SetAS()
    {
        bulletCoolDown = baseBulletCoolDown / ASmultiplier;
        float realMultiplier = (float)PlayerStats.sharedInstance.ASmultiplier / 100f;
        bulletCoolDown = bulletCoolDown / realMultiplier;      
    }

    public void SetForce()
    {
        Force = BaseForce * ForceMultiplier;
        Force = Force * PlayerStats.sharedInstance.ProjectileForce;
    }

    public void updateAS(int multiplier)
    {
        float realMultiplier = (float)multiplier / 100f;
        bulletCoolDown = baseBulletCoolDown / realMultiplier;
    }

    public void updateSize(int multiplier)
    {
        size = (float)multiplier / 100f * baseSize;
    }

    public virtual void setFirepoints()
    {
        
    }

    public virtual void setSideFirepoints()
    {

    }

    public virtual void ResetFirePoints()
    {

    }
    public virtual GameObject GetProjectile()
    {
        return null;
    }

   // public void updateForce(float multiplier)
   // {
   //     Force = BaseForce * multiplier;
   // }

}
