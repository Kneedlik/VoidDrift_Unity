using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    public PlayerInformation PlayerInformation;
    public static MasterManager Instance;
    private void Awake()
    {
        Instance = this;
    }


}
