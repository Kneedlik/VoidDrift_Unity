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
    public int HealthBonus;
    public float MsMultiplier;
    public float XpMultiplier;
    public float SizeMultiplier;
    public int ReviveBonus;
    public int ProjectileBonus;
    public float SummonDamageMultiplier;
    public int Rerols;
    public int SelectedLevel;

    public void CalculateStats(ProgressionState Progress)
    {
        DamageMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.Damage);
        AsMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.AttackSpeed);
        HealthBonus = 100 + (int)(100 * Progress.GetTotalIncrease(UpgradeConsts.Health));
        MsMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.Speed);
        XpMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.Xp);
        SizeMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.Area);
        ReviveBonus = (int)Progress.GetTotalIncrease(UpgradeConsts.ReviveIncrease);
        ProjectileBonus = (int)Progress.GetTotalIncrease(UpgradeConsts.ProjectileIncrease);
        SummonDamageMultiplier = 1f + Progress.GetTotalIncrease(UpgradeConsts.SummonDamage);
        Rerols = (int)Progress.GetTotalIncrease(UpgradeConsts.Rerols);
    }
}
