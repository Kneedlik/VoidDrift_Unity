using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiserBossAI : MonoBehaviour
{
    
    List<CruiserBossTurretAI> Turrets;
    public CheckTrigger LeftTurretActive;
    public CheckTrigger RightTurretActive;
    CruiserMissles Missles;

    public bool TurretsActive;


    void Start()
    {
        Missles = GetComponent<CruiserMissles>();
        //Missles.Fire();
    }

    void Update()
    {
        
    }


}
