using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerInformation", menuName = "PlayerInformation")]
public class PlayerInformation : ScriptableObject
{
    public int WeapeonId;
    public float DamageMultiplier;
    public float AsMultiplier;
    public float HealthMultiplier;
    public float MsMultiplier;
    public float XpMultiplier;
    public float SizeMultiplier;
    public int ReviveBonus;
    public int ProjectileBonus;
    public float SummonDamageMultiplier;
    public int Rerols;
    public int SelectedLevel;
    public float HealthRegen;
    public float GoldGain;
    public List<int> EquippedRunes = new List<int>();
    public int Keystone;

    //Enemy buffs
    public float MapEnemyHealthMultiplier;
    public float MapGoldMultiplier;
    public float MapDamageMultiplier;
    public bool HardMode;

    public void CalculateStats(ProgressionState Progress)
    {
        DamageMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.Damage);
        AsMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.AttackSpeed);
        HealthMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.Health);
        MsMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.Speed);
        XpMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.Xp);
        SizeMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.Area);
        ReviveBonus = (int)Progress.GetTotalIncrease(UpgradeConsts.Revives);
        ProjectileBonus = (int)Progress.GetTotalIncrease(UpgradeConsts.Projectiles);
        SummonDamageMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.SummonDamage);
        Rerols = (int)Progress.GetTotalIncrease(UpgradeConsts.Rerols);
        HealthRegen = Progress.GetTotalIncrease(UpgradeConsts.HealthRegen);
        GoldGain = 1f + Progress.GetTotalIncrease(UpgradeConsts.GoldGain);
    }
}
