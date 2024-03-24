using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterDeath : DeathFunc
{
    [SerializeField] List<Transform> ExploPos;
    [SerializeField] GameObject ExploObj;
    [SerializeField] GameObject ExploParticle;
    [SerializeField] float ExploDelay;

    public override void function()
    {
        justPatrol Patrol = GetComponent<justPatrol>();
        Patrol.speed = 0;

        Health health = GetComponent<Health>();
        health.DeathDelay = ExploDelay * ExploPos.Count + ExploDelay;

        StartCoroutine(functionCorutine()); 
    }

    IEnumerator functionCorutine()
    {
        yield return new WaitForSeconds(ExploDelay);

        for (int i = 0; i < ExploPos.Count; i++)
        {
            Instantiate(ExploObj, ExploPos[i]);
            Instantiate(ExploParticle, ExploPos[i].position,Quaternion.Euler(-90,0,0));

            yield return new WaitForSeconds(ExploDelay);
        }  
    }
}
