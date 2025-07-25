using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UpgradeConsts
{
    public const int Damage = 1;
    public const int Area = 2;
    public const int Speed = 3;
    public const int AttackSpeed = 4;
    public const int Health = 5;
    public const int Xp = 6;
    public const int Revives = 7;
    public const int Projectiles = 8;
    public const int SummonDamage = 9;
    public const int Rerols = 10;
    public const int HealthRegen = 11;
    public const int GoldGain = 12;

    public const float DamageIncrease = 10;
    public const float AreaIncrease = 10f;
    public const float SpeedIncrease = 7.5f;
    public const float AttackSpeedIncrease = 10f;
    public const float HealthIncrease = 20f;
    public const float XpIncrease = 6;
    public const int ReviveIncrease = 1;
    public const int ProjectileIncrease = 1;
    public const float SummonDamageIncrease = 10;
    public const int RerolIncrease = 1;
    public const float HealthRegenIncrease = 0.4f;
    public const float GoldGainIncrease = 10f;

    public static float GetIncrease(int Id)
    {
        switch (Id)
        {
            case Damage: return DamageIncrease;
            case Area: return AreaIncrease;
            case Speed: return SpeedIncrease;
            case AttackSpeed: return AttackSpeedIncrease;
            case Health: return HealthIncrease;
            case Xp: return XpIncrease;
            case Revives: return ReviveIncrease;
            case Projectiles: return ProjectileIncrease;
            case SummonDamage: return SummonDamageIncrease;
            case Rerols: return RerolIncrease;
            case HealthRegen: return HealthRegenIncrease;
            case GoldGain: return GoldGainIncrease;
            default: return 0;
        }
    }

    public static void SetColorToEmply(Image Img)
    {
        Img.color = new Color32(13,30,120,255);
    }

    public static void SetColorFill(Image Img)
    {
        Img.color = new Color32(0,220,255,255);
    }
}
