using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSummon : Summon
{
    [SerializeField] GameObject Line;
    [SerializeField] GameObject ConeObject;
    [SerializeField] GameObject ConeParticle; 
    public bool Aoe = false;

    public float beamDuration;
    float timeStamp;
    Transform target;

    public int ticks;
    public float tickDelay;

    // Update is called once per frame
    void Update()
    {
        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if(timeStamp <= 0)
        {
            if(target != null)
            {
                StartCoroutine(shoot());
                StartCoroutine(dealDamage());
                timeStamp = fireRate;
            }
            else if (setClosestTarget(out target) == false)
            {
                target = null;
            }
        }
    }

    IEnumerator shoot()
    {
       GameObject G = Instantiate(Line);
        LineRenderer line = G.GetComponent<LineRenderer>();
        LineFade lineDuration = G.GetComponent<LineFade>();
        lineDuration.duration = beamDuration;

        line.enabled = true;

        Vector3 pos = (target.position - transform.position).normalized;

        line.SetPosition(0,transform.position);
        
        line.SetPosition(1, transform.position + pos * 100);

        if(Aoe)
        {
            scaleSize();
            Invoke("SpawnCone", 0.3f);
            GameObject P = Instantiate(ConeParticle, transform.position, Quaternion.Euler(0, -90, 0));
            KnedlikLib.ScaleParticleByFloat(P, size, false);
            P.transform.LookAt(target);
            P.transform.rotation = Quaternion.Euler(P.transform.rotation.eulerAngles.y < 180 ? 270 - P.transform.rotation.eulerAngles.x : P.transform.rotation.eulerAngles.x, -90,0);

        }

        yield return new WaitForSeconds(beamDuration);
        Destroy(G);
    }

    void SpawnCone()
    {
        GameObject C = Instantiate(ConeObject, transform.position, Quaternion.Euler(0, 0, 0));
        KnedlikLib.ScaleParticleByFloat(C, size, false);
        C.transform.LookAt(target);
        C.transform.rotation = Quaternion.Euler(0, 0, C.transform.rotation.eulerAngles.y < 180 ? 270 - C.transform.rotation.eulerAngles.x : C.transform.rotation.eulerAngles.x - 180);
        explosion Explo = C.GetComponent<explosion>();
        Explo.damage = damage;
        Explo.function += Freeze;

        
    }

    void Freeze(GameObject target,int damage,ref int Damage)
    {
        BrittleSystem.Instance.ApplyBrittle(target, damage,ref Damage);
    }

    IEnumerator dealDamage()
    {
        for (int i = 0; i < ticks; i++)
        {
            if (target != null)
            {
                Vector3 pos = (target.position - transform.position).normalized;

                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, pos, 100);

                for (int j = 0; j < hit.Length; j++)
                {
                    if (hit[j].transform.GetComponent<Health>() != null)
                    {
                        Health health = hit[j].transform.GetComponent<Health>();

                        if (health != null)
                        {
                            int D = 0;
                            BrittleSystem.Instance.ApplyBrittle(hit[j].transform.gameObject, 0, ref D);

                            scaleSummonDamage();
                            health.TakeDamage(damage);
                        }
                    }
                }
                yield return new WaitForSeconds(beamDuration / (ticks + 1));
            }
        }
    }
}
