using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerPref", menuName = "PlayerPref")]

public class PlayerPrefs : ScriptableObject
{
    public int EquipedWeapeon;
    public int SelectedLevel;
    public int Keystone;
    public int RuneSlot1;
    public int RuneSlot2;
    public int RuneSlot3;
    public int RuneSlot4;
    public int RuneSlot5;
    public bool HardMode;
}
