using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiserConstants : MonoBehaviour
{
    public bool Active;
    public float ReloadTime;
    public float Delay;
    public float RotSpeed;
    public float BulletForce;
    public GameObject BulletPrefab;
    [SerializeField] List<CruiserBossTurretAI> Turrets;
    public float RandDelay;

    private void Start()
    {
        RandDelay = 0;
    }

    public void ActivateTurrets(float time)
    {
        for (int i = 0; i < Turrets.Count; i++)
        {
            Turrets[i].timeStamp = time;
        }
    }

    public void GenerateRandDelay()
    {
        if(RandDelay == 0)
        {
            RandDelay = Random.Range(0f,1.5f);
        }
    }
}
