using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDrone : Summon
{
    [SerializeField] Hovl_Laser Line;
    [SerializeField] Transform FirePoint;
    [SerializeField] float LineDuration;
    float TimeStamp;
    MiningLaser Laser;

    // Start is called before the first frame update
    void Start()
    {
        Laser = GameObject.FindWithTag("Weapeon").GetComponent<MiningLaser>();
        damage = baseDamage;
        fireRate = baseFireRate;
        scaleSummonDamage();
    }
    
    public bool Shoot()
    {
        Transform Target;
        if (Laser.DronesFinal == false)
        {
           if (KnedlikLib.FindClosestEnemy(transform, out Target))
           {
                StartCoroutine(ShootCorutine(Target));
                return true;
           }else return false;
        }else
        {
            if(KnedlikLib.FindClosestEnemyToCursor(Camera.main,out Target))
            {
                return true;
            }else return false;
        }
    }

    IEnumerator ShootCorutine(Transform Target)
    {
        Line.EnablePrepare();
        Line.SetUp(FirePoint.position, Target.position, false);
        Vector3 Dir = (Target.position - FirePoint.position).normalized;
        RaycastHit2D hitInfo = Physics2D.Raycast(FirePoint.position, Dir * 1000);
        if (hitInfo.collider != null)
        {
            //Debug.Log("Hit");
            Health health = hitInfo.transform.GetComponent<Health>();
            if(health != null)
            {
                int DamagePlus = damage;
                if(eventManager.OnImpact != null)
                {
                    eventManager.OnImpact(hitInfo.transform.gameObject,damage, ref DamagePlus);
                }

                Color32 Color = new Color32(0,0,0,0);
                if(eventManager.OnCrit != null)
                {
                    Color32 TempColor = eventManager.OnCrit(hitInfo.transform.gameObject, DamagePlus, ref DamagePlus);
                    Color32 BaseColor = new Color32(0, 0, 0, 0);
                    if (!TempColor.Equals(BaseColor))
                    {
                        Color = TempColor;
                    }
                }

                if(eventManager.PostImpact != null)
                {
                    eventManager.PostImpact(hitInfo.transform.gameObject, DamagePlus, ref DamagePlus);
                }

                if (Color.Equals(new Color32(0, 0, 0, 0)))
                {
                    health.TakeDamage(DamagePlus);
                }
                else
                {
                    //Debug.Log(Color);
                    health.TakeDamage(DamagePlus, Color);
                }
            }

        }
        float TimeStampTemp = LineDuration;
        while(TimeStampTemp > 0)
        {
            if (Target != null)
            {
                Line.SetUp(FirePoint.position, Target.position, false);
            }else
            {
                Line.SetUp(FirePoint.position, Vector3.zero, false);
            }
            TimeStampTemp -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        Line.DisablePrepare();
    }

    public override void scaleSummonDamage()
    {
        float pom = baseDamage * (PlayerStats.sharedInstance.SummonDamage / 100f);
        pom = pom * (PlayerStats.sharedInstance.damageMultiplier / 100f);
        damage = (int)pom;

    }
}
