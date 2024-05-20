using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class weapeon : MonoBehaviour
{
    public int Id;
    public List<upgrade> WeapeonUpgrades = new List<upgrade>();

    public Transform firePoint;
    public float ASmultiplier = 1;
    public int damage;
    public int baseDamage;
    [HideInInspector] public int startingDamage;
    public int extraDamage = 0;
    public float BaseForce = 10f;
    [HideInInspector] public float StartingForce;
    public float ForceMultiplier = 1;
    public float Force;
    public float size;
    public float baseSize = 1;
    [HideInInspector] public float StartingSize;
    public float baseCoolDown;
    [HideInInspector] public float StartingCooldown;
    public float CoolDown;
    public int pierce = 1;
    public float knockBack;
    public float knockBackMultiplier = 1;
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
        CoolDown = baseCoolDown / ASmultiplier;
        float realMultiplier = (float)PlayerStats.sharedInstance.ASmultiplier / 100f;
        CoolDown = CoolDown / realMultiplier;      
    }

    public void SetForce()
    {
        Force = BaseForce * ForceMultiplier;
        Force = Force * PlayerStats.sharedInstance.ProjectileForce;
    }

    public void updateAS(int multiplier)
    {
        float realMultiplier = (float)multiplier / 100f;
        CoolDown = baseCoolDown / realMultiplier;
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

    public void ResetStats()
    {
        baseDamage = startingDamage;
        BaseForce = StartingForce;
        baseSize = StartingSize;
        baseCoolDown = StartingCooldown;
        projectileCount = 1;
        sideProjectiles = 0;
        pierce = 0;
    }

    public void SetUpWeapeon()
    {
        startingDamage = baseDamage;
        StartingForce = BaseForce;
        StartingCooldown = baseCoolDown;
        StartingSize = baseSize;
        Force = BaseForce;
        updateDamage(100);
        updateSize(100);
        updateAS(100);
    }

    public void SetUpProjectile(BulletScript BulletDamage)
    {
        BulletDamage.setDamage(damage + extraDamage);
        BulletDamage.setArea(size);
        BulletDamage.setPierce(pierce);
        BulletDamage.setKnockBack(knockBack * knockBackMultiplier);
    }

   // public void updateForce(float multiplier)
   // {
   //     Force = BaseForce * multiplier;
   // }

}
