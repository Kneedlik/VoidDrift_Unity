using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Values", menuName = "Values")]

public class SettingsValues : ScriptableObject
{
    public bool IsFullScreen; 
    public int VSync;
   
}
