using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Library", menuName = "Libraries")]

public class AudioLibrary : ScriptableObject
{
    public List<Sound> clipList;
}
