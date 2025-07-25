using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RuneConst
{
    public const int CritChance1 = 1;
    public const int CritChance2 = 2;
    public const int CritDamage1 = 3;
    public const int CritDamage2 = 4;
    public const int DivineShield = 5;
    public const int HealthDamage = 6;
    public const int OneSummonBonus = 7;
    public const int DroneFireRate = 8;
    public const int StatusDamage1 = 9;
    public const int StatusDamage2 = 10;
    public const int StatusTick = 11;
    public const int SummonAndDamage = 12;
    public const int KillSpeed = 13;
    public const int ExtraDamage = 14;
    public const int OrbDropRate = 15;
    public const int Omega = 16;
    public const int Phoenix = 17;

    public const int CritChance1Increase = 25;
    public const int CritChance2Increase = 40;
    public const int CritDamage1Increase = 30;
    public const int CritDamage2Increase = 50;
    public const int HealthDamageIncrease = 1;
    public const int OneSummonBonusIncrease = 50;
    public const int DroneFireRateIncrease = 15;
    public const int StatusDamage1Increase = 20;
    public const int StatusDamage2Increase = 30;
    public const int StatusTickIncrease = 20;
    public const int SummonAndDamageIncrease = 25;
    public const int KillSpeedIncrease = 40;
    public const int ExtraDamageIncrease = 10;
    public const int OrbDropRateIncrease = 5;
    public const int OmegaIncrease = 20;
    public const int PhoenixIncrease = 10;

    public const int LethalTempoIncrease = 50;
    public const int NearDamageIncrease = 40;
    public const int NewStatusIncrease = 4;
    public const int LasersIncrease = 12;

    public static float GetRuneIncrease(int Id)
    {
        switch (Id)
        {
            case CritChance1: return CritChance1Increase;
            case CritChance2: return CritChance2Increase;
            case CritDamage1: return CritDamage1Increase;
            case CritDamage2: return CritDamage2Increase;
            case DivineShield: return 0;
            case HealthDamage: return HealthDamageIncrease;
            case OneSummonBonus: return OneSummonBonusIncrease;
            case DroneFireRate: return DroneFireRateIncrease;
            case StatusDamage1: return StatusDamage1Increase;
            case StatusDamage2: return StatusDamage2Increase;
            case StatusTick: return StatusTickIncrease;
            case SummonAndDamage: return SummonAndDamageIncrease;
            case KillSpeed: return KillSpeedIncrease;
            case OrbDropRate: return OrbDropRateIncrease;
            case Omega: return OmegaIncrease;
            case Phoenix: return PhoenixIncrease;
            case LethalTempo: return LethalTempoIncrease;
            case NearDamage: return NearDamageIncrease;
            case NewStatus: return NewStatusIncrease;
            case Lasers: return LasersIncrease;
            default: return 0;
        }
    }

    //KeyStones
    public const int LethalTempo = 21;
    public const int NearDamage = 22;
    public const int NewStatus = 23;
    public const int BlackHoles = 24;
    public const int Lasers = 25;
}
