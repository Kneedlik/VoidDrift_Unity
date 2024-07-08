using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManager : MonoBehaviour
{
    public delegate void ImpactEffect(GameObject target, int damage, ref int scaledDamage);
    public delegate Color32 ImpactEffectColor(GameObject target, int damage, ref int scaledDamage);
    public delegate void ImpactProjectile(GameObject target, GameObject bullet);
    public delegate void DamageEffect(int amount);
    public delegate void OnFireEffect(GameObject weapeon);
    public delegate void OnFireAllEffect(GameObject weapeon,GameObject bullet);
    public delegate void OnKillEffect(GameObject target);
  //  public delegate void OnDamageEffect(int damage,GameObject target);
    //  public static event ImpactEffect OnImpact;
    public static ImpactEffect OnImpact;
    public static ImpactEffect SummonOnImpact;
    public static ImpactEffect PostImpact;
    public static ImpactEffectColor OnCrit;
    public static ImpactProjectile ImpactGunOnly;
    public static ImpactEffect ImpactGunOnlyHitScan;

    public  static OnFireEffect OnFire;
    public static OnFireAllEffect OnFireAll;
    public static DamageEffect OnDamage;
    public static DamageEffect OnDamageEnemy;
    public static OnKillEffect OnKill;
  //  public static OnDamageEffect OnDamage;

    private void Start()
    {
        //  eventManager.OnImpact += poisonSystem.sharedInstance.Poison;
        ClearAllEffects();

    }

    public static void ClearAllEffects()
    {
        OnImpact = null;
        SummonOnImpact = null;
        OnFire = null;
        PostImpact = null;
        OnDamage = null;
        OnDamageEnemy = null;
        OnKill = null;
        ImpactGunOnly = null;
    }
}
