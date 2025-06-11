using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class TorsoBossAI : MonoBehaviour
{
    Transform Player;
    float timeStamp;
    bool finished;
    Rigidbody2D rb;
    int OnCoolDown;

    [SerializeField] float FinishDelay;
    [SerializeField] float ChanelingDuration;
    [SerializeField] Transform FirePoint;
    [SerializeField] ParticleSystem Chaneling;

    //Movement
    [SerializeField] float SpeedFast;
    [SerializeField] float MaxSpeedFast;
    [SerializeField] float SpeedSlow;
    [SerializeField] float MaxSpeedSlow;
    [SerializeField] float DesiredDistance;

    //Attack1
    [SerializeField] int WaweCountA1;
    [SerializeField] int DamageA1;
    [SerializeField] float CooldownA1;
    [SerializeField] float WaweDelayA1;
    [SerializeField] GameObject ProjectilePrefabA1;
    [SerializeField] int ProjectilesInBurstA1;
    [SerializeField] float BurstDelayA1;
    [SerializeField] float ProjectileForceA1;

    //Attack2
    [SerializeField] GameObject ProjectilePrefabA2;
    [SerializeField] float CoolDownA2;
    [SerializeField] float FireRateA2;
    [SerializeField] float StartAngleA2;
    [SerializeField] float AngleIncreaseA2;
    [SerializeField] float EndAngleA2;

    //Attack3
    [SerializeField] GameObject ExplosionPrefabA3;
    [SerializeField] int ExplosionCountA3;
    [SerializeField] float ExplosionDelayA3;
    [SerializeField] float MaxRangeXA3;
    [SerializeField] float MaxRangeYA3;
    [SerializeField] float CoolDownA3;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        finished = true;
        //Attack3();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (timeStamp <= 0 && finished)
        {
            finished = false;
            DecideAttacl();
        }
    }

    private void FixedUpdate()
    {
        if (finished)
        {
            float Distance = Vector3.Distance(transform.position, Player.position);
            Vector3 dir = Player.position - transform.position;

            if (Distance <= DesiredDistance)
            {
                rb.AddForce(dir * SpeedSlow, ForceMode2D.Force);
                KnedlikLib.SetMaxSpeed(MaxSpeedSlow, rb);
                //Debug.Log('1');
            }
            else
            {
                rb.AddForce(dir * SpeedFast, ForceMode2D.Force);
                KnedlikLib.SetMaxSpeed(MaxSpeedFast, rb);
                //Debug.Log('2');
            }
        }
    }

    public void DecideAttacl()
    {
        int Rand;
        rb.velocity = Vector3.zero;

        switch (OnCoolDown)
        {
            case 0:
                Rand = Random.Range(1, 4);
                if (Rand == 1)
                {
                    Attack1();
                    OnCoolDown = 1;
                }
                else if (Rand == 2)
                {
                    Attack2();
                    OnCoolDown = 2;
                }
                else if (Rand == 3)
                {
                    Attack3();
                    OnCoolDown = 3;
                }
                break;
            case 1:
                Rand = Random.Range(1, 3);
                if (Rand == 1)
                {
                    Attack2();
                    OnCoolDown = 2;
                }
                else if (Rand == 2)
                {
                    Attack3();
                    OnCoolDown = 3;
                }
                break;
            case 2:
                Rand = Random.Range(1, 3);
                if (Rand == 1)
                {
                    Attack1();
                    OnCoolDown = 1;
                }
                else if (Rand == 2)
                {
                    Attack3();
                    OnCoolDown = 3;
                }
                break;
            case 3:
                Rand = Random.Range(1, 3);
                if (Rand == 1)
                {
                    Attack1();
                    OnCoolDown = 1;
                }
                else if (Rand == 2)
                {
                    Attack2();
                    OnCoolDown = 2;
                }
                break;

        }
    }

    IEnumerator Attack1Corrutine()
    {
        finished = false;
        Chaneling.Play();
        yield return new WaitForSeconds(ChanelingDuration);

        float Offset = 360 / (ProjectilesInBurstA1 * 4);
        Offset = Offset / 2;
        float OffsetTemp = 15;
        float StartTemp = OffsetTemp;

        for (int l = 0; l < 3; l++)
        {
            for (int i = 0; i < WaweCountA1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < ProjectilesInBurstA1; k++)
                    {
                        OffsetTemp += Offset;
                        GameObject ProjectileTemp = Instantiate(ProjectilePrefabA1, FirePoint.transform.position, Quaternion.Euler(0, 0, OffsetTemp));
                        ProjectileTemp.GetComponent<foddeBullet>().damage = DamageA1;
                        Rigidbody2D rbTemp = ProjectileTemp.GetComponent<Rigidbody2D>();
                        rbTemp.velocity = ProjectileTemp.transform.up * ProjectileForceA1;
                    }
                    OffsetTemp += 45;
                }

                yield return new WaitForSeconds(WaweDelayA1);
            }

            yield return new WaitForSeconds(BurstDelayA1);

            StartTemp = StartTemp + 45;
            OffsetTemp = StartTemp;
        }

        Chaneling.Pause();
        Chaneling.Clear();

        yield return new WaitForSeconds(FinishDelay);

        finished = true;
        timeStamp = CooldownA1;
    }

    void Attack1()
    {
        StartCoroutine(Attack1Corrutine());
    }

    IEnumerator Attack2Corrutine()
    {
        finished = false;
        Chaneling.Play();
        yield return new WaitForSeconds(ChanelingDuration);

        Vector2 Temp = Player.transform.position - FirePoint.position;
        float PlayerAngle = Mathf.Atan2(Temp.y,Temp.x) * Mathf.Rad2Deg - 90f;
        Debug.Log(PlayerAngle);
        float CurrentAngle = PlayerAngle + StartAngleA2;
        float OtherAngle = PlayerAngle - StartAngleA2;
        Debug.Log(CurrentAngle);
        float DesiredAngle = PlayerAngle - EndAngleA2;

        while (CurrentAngle > DesiredAngle)
        {
            Instantiate(ProjectilePrefabA2, FirePoint.transform.position, Quaternion.Euler(0,0,CurrentAngle));
            Instantiate(ProjectilePrefabA2, FirePoint.transform.position, Quaternion.Euler(0, 0, OtherAngle));
            CurrentAngle -= AngleIncreaseA2;
            OtherAngle += AngleIncreaseA2;
            yield return new WaitForSeconds(FireRateA2);
        }

        Chaneling.Pause();
        Chaneling.Clear();

        yield return new WaitForSeconds(FinishDelay);

        finished = true;
        timeStamp = CoolDownA2;
    }

    void Attack2()
    {
        StartCoroutine(Attack2Corrutine());
    }

    IEnumerator Attack3Corrutine()
    {
        finished = false;
        Chaneling.Play();
        yield return new WaitForSeconds(ChanelingDuration);

        for (int i = 0; i < ExplosionCountA3; i++)
        {
            Vector3 ExploPos = new Vector3(Random.Range(MaxRangeXA3 * -1, MaxRangeXA3), Random.Range(MaxRangeYA3 * -1, MaxRangeYA3), 0);
            ExploPos += Player.position;
            Instantiate(ExplosionPrefabA3,ExploPos,Quaternion.Euler(0,0,0));
            yield return new WaitForSeconds(ExplosionDelayA3);
        }

        
        Chaneling.Pause();
        Chaneling.Clear();

        yield return new WaitForSeconds(FinishDelay);

        finished = true;
        timeStamp = CoolDownA3;
    }

    void Attack3()
    { 
        StartCoroutine(Attack3Corrutine());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DesiredDistance);
    }
}
