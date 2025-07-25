using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    public static RuneManager instance;
    public ProgressionState progressionState;

    void Start()
    {
        instance = this;
    }

}
