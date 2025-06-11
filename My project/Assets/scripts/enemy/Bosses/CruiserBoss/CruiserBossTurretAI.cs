using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiserBossTurretAI : MonoBehaviour
{
    CruiserConstants Const;
    [SerializeField] GameObject Turret1;
    [SerializeField] GameObject Turret2;
    [SerializeField] List<Transform> Barrels;
    [SerializeField] List<Transform> FirePointCentre;
    public CheckTrigger Range;
    public float timeStamp;
    int Index;
    float ReloadTimeAdjusted;

    Transform Player;
    Rigidbody2D PlayerRB;
    CruiserBossAI BossAI;

    float baseRot;
    float desiredRot1;
    float desiredRot2;

    Quaternion Q1;
    Quaternion Q2;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        PlayerRB = Player.GetComponent<Rigidbody2D>();
        baseRot = Turret1.transform.rotation.eulerAngles.z + 90;
        desiredRot1 = baseRot;
        desiredRot2 = baseRot;
        BossAI = GetComponent<CruiserBossAI>();
        Index = 0;
        Const = GetComponent<CruiserConstants>();
        ReloadTimeAdjusted = Const.ReloadTime;
    }

    void Update()
    {
        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }


        if (Range == null || Range.Colliding)
        {
            //if (Const.Active)
            //{
            if (KnedlikLib.InterceptionPoint(Player.position, FirePointCentre[0].position, PlayerRB.velocity, Const.BulletForce, out var direction1))
            {
                float angle = Mathf.Atan2(direction1.y, direction1.x) * Mathf.Rad2Deg - 90;
                //transform.rotation = Quaternion.Euler(0, 0, angle);
                desiredRot1 = angle;
            }
            else
            {
                Vector2 pom = Player.position - FirePointCentre[0].position;
                pom = pom.normalized;
                float angle = Mathf.Atan2(pom.y, pom.x) * Mathf.Rad2Deg - 90;
                desiredRot1 = angle;
            }


            if (KnedlikLib.InterceptionPoint(Player.position, FirePointCentre[1].position, PlayerRB.velocity, Const.BulletForce, out var direction2))
            {
                float angle = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg - 90;
                //transform.rotation = Quaternion.Euler(0, 0, angle);
                desiredRot2 = angle;
            }
            else
            {
                //transform.rotation = Quaternion.Euler(0, 0, baseRot);
                Vector2 pom = Player.position - FirePointCentre[1].position;
                pom = pom.normalized;
                float angle = Mathf.Atan2(pom.y, pom.x) * Mathf.Rad2Deg - 90;
                desiredRot2 = angle;
            }

            if (Const.Active)
            {
                if (timeStamp <= 0)
                {
                    if (Index == 0 || Index == 2)
                    {
                        if (Turret1.transform.rotation == Q1)
                        {
                            float Rand = Random.Range(0.1f, 1.2f);
                            Invoke("Fire", Rand);
                            if (Index == 3)
                            {
                                timeStamp = Const.ReloadTime;
                            }
                            else
                            {
                                timeStamp = Const.Delay;
                            }
                        }
                    }
                    else
                    {
                        if (Turret2.transform.rotation == Q2)
                        {
                            float Rand = Random.Range(0.1f, 0.6f);
                            Invoke("Fire", Rand);
                            if (Index == 3)
                            {
                                timeStamp = ReloadTimeAdjusted;
                                ReloadTimeAdjusted = Const.ReloadTime;
                            }
                            else
                            {
                                timeStamp = Const.Delay + Rand;
                                ReloadTimeAdjusted -= Rand;
                            }
                        }
                    }
                }
            }

            //}
            //else
            //{
            //    desiredRot1 = baseRot;
            //    desiredRot2 = baseRot;
            //}
        }
    }

    private void FixedUpdate()
    {
        if(Turret1.transform.rotation.eulerAngles.z != desiredRot1)
        {
            Quaternion desiredQuaternion = Quaternion.Euler(0, 0, desiredRot1 -90);
            Turret1.transform.rotation = Quaternion.RotateTowards(Turret1.transform.rotation, desiredQuaternion, Const.RotSpeed * Time.deltaTime);
            Q1 = desiredQuaternion;
        }

        if (Turret2.transform.rotation.eulerAngles.z != desiredRot2)
        {
            Quaternion desiredQuaternion = Quaternion.Euler(0, 0, desiredRot2 - 90);
            Turret2.transform.rotation = Quaternion.RotateTowards(Turret2.transform.rotation, desiredQuaternion, Const.RotSpeed * Time.deltaTime);
            Q2 = desiredQuaternion;
        }
    }

    public void Fire()
    {
        GameObject Bullet;
        Bullet = Instantiate(Const.BulletPrefab, Barrels[Index].position, Barrels[Index].rotation);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        
        rb.velocity = Barrels[Index].up * Const.BulletForce;

        IncreaseIndex();


    }
    
    public void IncreaseIndex()
    {
        if(Index < 3)
        {
            Index++;
        }else
        {
            Index = 0;
        }
    }
}
