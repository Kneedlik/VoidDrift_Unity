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
    public float damageMultiplier = 1f;
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
    public int projectileCount;
    public int sideProjectiles = 0;

     public List<GameObject> CubeList = new List<GameObject>();
     public List<GameObject> SideCubeList = new List<GameObject>();


    public void updateDamage(int multiplier)
    {
        float realMultiplier = (float)multiplier / 100f;
        float pomDamage = (float)baseDamage * damageMultiplier * MasterManager.Instance.PlayerInformation.DamageMultiplier;
        pomDamage = pomDamage * realMultiplier;
        damage = (int)pomDamage;
    }

    public void SetAS()
    {
        CoolDown = baseCoolDown / ASmultiplier;
        float realMultiplier = (float)PlayerStats.sharedInstance.ASmultiplier / 100f;
        CoolDown = CoolDown / realMultiplier;
        CoolDown = CoolDown / MasterManager.Instance.PlayerInformation.AsMultiplier;
    }

    public void SetForce()
    {
        Force = BaseForce * ForceMultiplier;
        Force = Force * PlayerStats.sharedInstance.ProjectileForce;
    }

    public void updateAS(int multiplier)
    {
        float realMultiplier = (float)multiplier / 100f;
        realMultiplier = realMultiplier * ASmultiplier * MasterManager.Instance.PlayerInformation.AsMultiplier;
        CoolDown = baseCoolDown / realMultiplier;
    }

    public virtual void updateSize(int multiplier)
    {
        size = (float)multiplier / 100f;
        size = size * baseSize * MasterManager.Instance.PlayerInformation.SizeMultiplier;
    }

    public virtual void setFirepoints()
    {
        
    }

    public virtual void setSideFirepoints()
    {
        for (int i = 0; i < CubeList.Count; i++)
        {
            Destroy(CubeList[i]);
        }
        CubeList.Clear();
    }

    public virtual void ResetFirePoints()
    {
        for (int i = 0; i < CubeList.Count; i++)
        {
            Destroy(CubeList[i]);
        }
        CubeList.Clear();
    }
    public virtual void ResetSideFirePoints()
    {
        for (int i = 0; i < SideCubeList.Count; i++)
        {
            Destroy(SideCubeList[i]);
        }
        SideCubeList.Clear();
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
