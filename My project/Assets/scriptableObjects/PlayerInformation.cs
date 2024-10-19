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
}
