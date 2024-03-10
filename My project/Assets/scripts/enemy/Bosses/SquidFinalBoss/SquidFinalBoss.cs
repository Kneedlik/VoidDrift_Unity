using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SquidFinalBoss : MonoBehaviour
{
    Transform Player;
    float timeStamp;
    bool finished;
    int OnCoolDown;
    bool Chaneling;
    [SerializeField] float RandMin;
    [SerializeField] float RandMax;

    //Movement
    [SerializeField] float SpeedFast;
    [SerializeField] float MaxSpeedFast;
    [SerializeField] float SpeedSlow;
    [SerializeField] float MaxSpeedSlow;
    [SerializeField] float DesiredDistance;
    Rigidbody2D rbSelf;

    //Attack1
    [SerializeField] GameObject BulletPrefabA1;
    [SerializeField] int projectileCountA1;
    [SerializeField] float ProjectileForceA1;
    [SerializeField] int numberOfBurstsA1;
    [SerializeField] float BurstDelayA1;
    [SerializeField] Transform FirePointA1;
    [SerializeField] int damageA1;
    [SerializeField] float CollDownA1;
    bool flip;

    //Attack2
    [SerializeField] int PortalAmountA2;
    [SerializeField] GameObject PortalPrefabA2;
    [SerializeField] float BoxMinHightA2;
    [SerializeField] float BoxMaxHightA2;
    [SerializeField] float BoxMinWidthA2;
    [SerializeField] float BoxMaxWidthA2;
    [SerializeField] float CoolDownA2;

    //Attack3
    [SerializeField] int projectileCountA3;
    [SerializeField] float CoolDownA3;
    [SerializeField] int DamageA3;
    [SerializeField] GameObject LightNingBoltPrefabA3;
    [SerializeField] float FireDelayA3;
    [SerializeField] float FireDurationA3;
    [SerializeField] float CoolDownShortA3;
    [SerializeField] GameObject WarningIndicatorPrefab;
    bool AttackActiveA3;
    bool TargetingA3;
    bool LockedA3;
    bool FireingA3;
    bool WidthDoneA3;
    float WidthA3;
    int IndexA3;
    Camera MainCamera;
    GameObject WarningIndicator;
    Vector3 pos;
 


    // Start is called before the first frame update
    void Start()
    {
        IndexA3 = 0;
       // LineA3.enabled = false;
        finished = true;
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        rbSelf = gameObject.GetComponent<Rigidbody2D>();

        //Attack2();
    }

    // Update is called once per frame
    void Update()
    {

        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if(timeStamp <= 0 && finished == true)
        {
            //finished = false;
           // DecideAttacl();
        }

        if(AttackActiveA3)
        {
            if (TargetingA3)
            {
              
                if(timeStamp > 0)
                {

                }else
                {
                    Vector3 velocity = Player.GetComponent<Rigidbody2D>().velocity;
                    timeStamp = FireDelayA3;
                    pos = Player.position + velocity * FireDelayA3;

                    Vector3 TempPos = MainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
                    Vector3 TempPos2 = Player.position + velocity * FireDelayA3;
                    TempPos = new Vector3(TempPos2.x, TempPos.y, TempPos2.z);
                    WarningIndicator = Instantiate(WarningIndicatorPrefab, TempPos, Quaternion.Euler(0, 0, 0));

                    LockedA3 = true;
                    TargetingA3 = false;

                }

            }

           
            if (LockedA3)
            {
                //Vector3 TempPos = MainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

                if (timeStamp > 0)
                {
                    //WarningIndicator.transform.position = new Vector3(pos.x, TempPos.y, 0);
                }
                else
                {
                    GameObject BoltPrefab = Instantiate(LightNingBoltPrefabA3, transform.position, Quaternion.Euler(0, 0, 0));
                    LightningBolt bolt = BoltPrefab.GetComponent<LightningBolt>();

                    bolt.StartPosition = WarningIndicator.transform.position;
                    bolt.EndPosition = WarningIndicator.transform.position - new Vector3(0, 1000, 0); 

                    RaycastHit2D[] hitInfo = Physics2D.RaycastAll(WarningIndicator.transform.position, new Vector3(0,-1,0));
                    for (int i = 0; i < hitInfo.Length; i++)
                    {
                        //Debug.DrawRay(firePoint.position, temp * 100, Color.green, 100);

                        if (hitInfo[i].transform.GetComponent<plaerHealth>() != null)
                        {
                            plaerHealth Health = hitInfo[i].transform.GetComponent<plaerHealth>();
                            Health.TakeDamage(DamageA3);
                        }
                    }

                    LockedA3 = false;
                    FireingA3 = true;
                    timeStamp = FireDurationA3;
                }
            
            } 
            
            if(FireingA3)
            {
                if(timeStamp < 0)
                {
                    FireingA3 = false;

                    if (KnedlikLib.IncreaseIndex(ref IndexA3,projectileCountA3) == false)
                    {
                        finished = true;
                        float rand = Random.Range(RandMin, RandMax);
                        timeStamp = CoolDownA3 + rand;
                        AttackActiveA3 = false;
                    }else
                    {
                        timeStamp = CoolDownShortA3;
                        TargetingA3 = true;
                    }
                }


            }
        }

    }

    private void FixedUpdate()
    {
        if(finished && timeStamp <= 0)
        {
            float Distance = Vector3.Distance(transform.position, Player.position);
            Vector3 dir = Player.position - transform.position;

            if (Distance <= DesiredDistance)
            {
                rbSelf.AddForce(dir * SpeedSlow,ForceMode2D.Force);
                KnedlikLib.SetMaxSpeed(MaxSpeedSlow, rbSelf);
                //Debug.Log('1');
            }else
            {
                rbSelf.AddForce(dir * SpeedFast,ForceMode2D.Force);
                KnedlikLib.SetMaxSpeed(MaxSpeedFast, rbSelf);
                //Debug.Log('2');
            }
        }
    }

    public void DecideAttacl()
    {
        int Rand;

        switch(OnCoolDown)
        {
            case 0:
                Rand = Random.Range(1,3);
                if(Rand == 1)
                {
                    Attack1();
                    OnCoolDown = 1;
                }else if(Rand == 2)
                {
                    Attack2();
                    OnCoolDown = 2;
                }else if(Rand == 3)
                {
                    Attack3();
                    OnCoolDown = 3;
                }
                break;
            case 1:
                Rand = Random.Range(1,2);
                if(Rand == 1)
                {
                    Attack2();
                    OnCoolDown = 2;
                }else if(Rand == 2)
                {
                    Attack3();
                    OnCoolDown = 3;
                }
                break;
            case 2:
                Rand = Random.Range(1, 2);
                if(Rand == 1)
                {
                    Attack1();
                    OnCoolDown = 1;
                }else if (Rand == 2)
                {
                    Attack3();
                    OnCoolDown = 3;
                }
                break;
            case 3:
                Rand = Random.Range(1, 2);
                if(Rand == 1)
                {
                    Attack1();
                    OnCoolDown = 1;
                }else if (Rand == 2)
                {
                    Attack2();
                    OnCoolDown = 2;
                }
                break;

        }
    }

    IEnumerator Attack1Corutine()
    {
        finished = false;

        for(int i = 0; i < numberOfBurstsA1; i++) 
        {
            float angle = 360 / projectileCountA1;
            float pom = angle;

            if (flip)
            {
                angle += pom / 2;
                flip = false;
            }else
            {
                flip = true;
            }

            for (int j = 0; j < projectileCountA1; j++)
            {
                GameObject B = Instantiate(BulletPrefabA1, FirePointA1.position, Quaternion.Euler(0, 0, angle));
                B.GetComponent<foddeBullet>().damage = damageA1;
                Rigidbody2D RB = B.GetComponent<Rigidbody2D>();
                RB.velocity = B.transform.up * ProjectileForceA1;

                angle += pom;
            }

            yield return new WaitForSeconds(BurstDelayA1);
        }

        float rand = Random.Range(RandMin, RandMax);
        timeStamp = CollDownA1 + rand;
        finished = true;
    }

    public void Attack1()
    {
        StartCoroutine(Attack1Corutine());
    }

    IEnumerator Attack2Corutine()
    {
        finished = false;
        yield return new WaitForSeconds(1);

        List<Vector3> Points = new List<Vector3>();
        for (int i = 0; i < PortalAmountA2; i++)
        {
            float RandX = Random.Range(BoxMinWidthA2, BoxMaxWidthA2);
            float RandY = Random.Range(BoxMinHightA2, BoxMaxHightA2);
            Vector3 pos = KnedlikLib.GenerateRandPosition(transform.position, RandX, RandY);
            Points.Add(new Vector3(pos.x, pos.y, pos.z));
        }

        for (int i = 0; i < Points.Count; i++)
        {
            Instantiate(PortalPrefabA2, Points[i], Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(2);
        finished = true;
        float rand = Random.Range(RandMin, RandMax);
        timeStamp = CoolDownA2 + rand;
    }

    public void Attack2()
    {
        StartCoroutine(Attack2Corutine());
    }

    public void Attack3()
    {
        AttackActiveA3 = true;
        TargetingA3 = true;
        finished = false;
        //StartCoroutine(Attack3Corutine());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position,new Vector3(BoxMaxWidthA2,BoxMaxHightA2,0));
        Gizmos.DrawWireCube(transform.position, new Vector3(BoxMinWidthA2, BoxMinHightA2, 0));
        Gizmos.DrawWireSphere(transform.position, DesiredDistance);
    }


}
