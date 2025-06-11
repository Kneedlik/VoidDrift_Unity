using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusatAI : MonoBehaviour
{
    //Attack1
    [SerializeField] Transform FirePoint1A1;
    [SerializeField] Transform FirePoint2A1;
    [SerializeField] int ProjectilesInWaweA1;
    [SerializeField] int WawesA1;
    [SerializeField] float WaweDelayA1;
    [SerializeField] float OffsetA1;
    [SerializeField] float CoolDownA1;
    [SerializeField] float ProjectileForceA1;
    [SerializeField] GameObject ProjectilePrefabA1;

    //Attack2
    [SerializeField] List<Transform> FirePointsA2 = new List<Transform>();
    [SerializeField] GameObject ProjectilePrefabA2;
    [SerializeField] GameObject WarningPrefabA2;
    [SerializeField] float ChargeDelayA2;
    [SerializeField] float newProjectileDelayA2;
    [SerializeField] float ProjectileForceA2;
    [SerializeField] float CoolDownA2;
    [SerializeField] GameObject PortalPrefabA2;
    [SerializeField] float RandomOffsetA2;

    //Attack3
    [SerializeField] Transform FirePoint1A3;
    [SerializeField] Transform FirePoint2A3;
    [SerializeField] int ProjectilesInWaweA3;
    [SerializeField] int WawesCountA3;
    [SerializeField] float WaweDelayA3;
    [SerializeField] GameObject ProjectilePrefabA3;
    [SerializeField] float CoolDownA3;
    [SerializeField] float OffsetA3;

    Transform Player;
    Rigidbody2D PlayerRb;
    float TimeStamp;
    bool Attacking;
    int OnCoolDown;
    FusatMovement Movement;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        PlayerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        Movement = GetComponent<FusatMovement>();
        OnCoolDown = 2;

        //StartCoroutine(Attack3());
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if (TimeStamp <= 0 && Attacking == false && Movement.Ready)
        {
            Attacking = true;
            DecideAttack();
        }
    }

    void DecideAttack()
    {
        int Rand;
        switch(OnCoolDown)
        {
            case 1:
                Rand = Random.Range(1, 3);
                if(Rand == 1)
                {
                    OnCoolDown = 2;
                    StartCoroutine(Attack2());
                }
                else if (Rand == 2)
                {
                    OnCoolDown = 3;
                    StartCoroutine(Attack3());
                }  
                break;
            case 2:
                Rand = Random.Range(1, 3);
                if (Rand == 1)
                {
                    OnCoolDown = 1;
                    StartCoroutine(Attack1());
                }
                else if (Rand == 2)
                {
                    OnCoolDown = 2;
                    StartCoroutine(Attack3());
                }
                break;
            case 3:
                Rand = Random.Range(1, 3);
                if (Rand == 1)
                {
                    OnCoolDown = 1;
                    StartCoroutine(Attack1());
                }
                else if (Rand == 2)
                {
                    OnCoolDown = 2;
                    StartCoroutine(Attack2());
                }
                break;
        }
    }

    IEnumerator Attack1()
    {
        GameObject Obj;
        float offsetTemp = OffsetA1 / 2;
        bool Flip = false;

        for(int i = 0; i < WawesA1; i++)
        {
            for (int j = 0;j < ProjectilesInWaweA1; j++)
            {
                if (Flip)
                {
                    Obj = Instantiate(ProjectilePrefabA1, FirePoint1A1.position, Quaternion.Euler(0, 0, FirePoint1A1.rotation.eulerAngles.z - offsetTemp));
                    offsetTemp += OffsetA1;
                    Flip = false;
                    //Debug.Log(FirePoint1A1.rotation.eulerAngles.z - offsetTemp);
                }
                else
                {
                    Obj = Instantiate(ProjectilePrefabA1, FirePoint1A1.position, Quaternion.Euler(0, 0, FirePoint1A1.rotation.eulerAngles.z + offsetTemp));
                    Flip = true;
                    //Debug.Log(FirePoint1A1.rotation.eulerAngles.z + offsetTemp);
                }
                Rigidbody2D rb = Obj.GetComponent<Rigidbody2D>();
                rb.velocity = Obj.transform.up * ProjectileForceA1;
            }

            offsetTemp = OffsetA1;
            Flip = false;

            for (int j = 0; j < ProjectilesInWaweA1; j++)
            {
                if (Flip)
                {
                    Obj = Instantiate(ProjectilePrefabA1, FirePoint2A1.position, Quaternion.Euler(0, 0, FirePoint2A1.rotation.eulerAngles.z - offsetTemp));
                    offsetTemp += OffsetA1;
                    Flip = false;
                }
                else
                {
                    Obj = Instantiate(ProjectilePrefabA1, FirePoint2A1.position, Quaternion.Euler(0, 0, FirePoint2A1.rotation.eulerAngles.z + offsetTemp));
                    Flip = true;
                }
                Rigidbody2D rb = Obj.GetComponent<Rigidbody2D>();
                rb.velocity = Obj.transform.up * ProjectileForceA1;
            }

            yield return new WaitForSeconds(WaweDelayA1);
        }

        TimeStamp = CoolDownA1;
        Attacking = false;
    }

    IEnumerator Attack2()
    {
        for (int i = 0; i < FirePointsA2.Count; i++)
        {
            GameObject Portal = Instantiate(PortalPrefabA2, FirePointsA2[i].position,Quaternion.Euler(0,0,0));

            Vector3 velTemp = PlayerRb.velocity;
            Vector3 pos = Player.transform.position + velTemp * (ChargeDelayA2 * 0.6f);
            Vector3 Rand = new Vector3(Random.Range(RandomOffsetA2 * -1,RandomOffsetA2),Random.Range(RandomOffsetA2 * -1,RandomOffsetA2),0);
            pos = pos + Rand;
            
            LineRenderer Line = Instantiate(WarningPrefabA2).GetComponent<LineRenderer>();
            pos = (pos - FirePointsA2[i].position).normalized;
            Line.SetPosition(0, FirePointsA2[i].position);
            Line.SetPosition(1, FirePointsA2[i].position + pos * 1000);
            yield return new WaitForSeconds(newProjectileDelayA2);

            if(i == FirePointsA2.Count - 1)
            {
                StartCoroutine(FireLaser(Line,Portal, true));
            }
            else
            {
                StartCoroutine(FireLaser(Line,Portal, false));
            }
        }
    }

    IEnumerator FireLaser(LineRenderer Line,GameObject Portal,bool Final)
    {
        yield return new WaitForSeconds(ChargeDelayA2);
        Rigidbody2D rb = Instantiate(ProjectilePrefabA2,Line.GetPosition(0),Quaternion.Euler(0,0,0)).GetComponent<Rigidbody2D>();
        KnedlikLib.lookAt2d(rb.transform, Line.GetPosition(1), -90);

        Vector3 dir = Line.GetPosition(1) - Line.GetPosition(0);
        rb.velocity = dir.normalized * ProjectileForceA2;
        Destroy(Portal);
        Destroy(Line.gameObject);

        if (Final)
        {
            Attacking = false;
            TimeStamp = CoolDownA2;
            Debug.Log("Finished attack");
        }
    }

    IEnumerator Attack3()
    {
        GameObject Obj;
        float offsetTemp = OffsetA3 / 2;
        bool Flip = false;


        for (int i = 0; i < WawesCountA3;i++)
        {
            for (int j = 0; j < ProjectilesInWaweA3;j++)
            {
                if(Flip)
                {
                    Obj = Instantiate(ProjectilePrefabA3, FirePoint1A3.position, Quaternion.Euler(0, 0, FirePoint1A3.rotation.eulerAngles.z + offsetTemp));
                    Flip = false;
                    offsetTemp += OffsetA3;
                }else
                {
                    Obj = Instantiate(ProjectilePrefabA3, FirePoint1A3.position, Quaternion.Euler(0, 0,FirePoint1A3.rotation.eulerAngles.z - offsetTemp));
                    Flip = true;
                }
            }

            offsetTemp = OffsetA3;
            Flip = false;

            for (int j = 0; j < ProjectilesInWaweA3; j++)
            {
                if (Flip)
                {
                    Flip = false;
                    offsetTemp += OffsetA3;
                    Instantiate(ProjectilePrefabA3, FirePoint2A3.position, Quaternion.Euler(0, 0, FirePoint2A3.rotation.eulerAngles.z - offsetTemp));
                }
                else
                {
                    Instantiate(ProjectilePrefabA3, FirePoint2A3.position, Quaternion.Euler(0, 0, FirePoint2A3.rotation.eulerAngles.z + offsetTemp));
                    Flip = true;
                }
            }
            yield return new WaitForSeconds(WaweDelayA3);

        }

        TimeStamp = CoolDownA3;
        Attacking = false;
    }
}
