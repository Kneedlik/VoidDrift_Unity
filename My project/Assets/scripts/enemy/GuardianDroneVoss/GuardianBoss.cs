using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianBoss : MonoBehaviour
{
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] GameObject WarningPrefab;
    [SerializeField] GameObject CubePrefab;
    [SerializeField] Transform FirePoint;

    //Attack1
    [SerializeField] int DamageA1;
    [SerializeField] float CoolDownA1;
    [SerializeField] float WarningDelayA1;
    [SerializeField] float AttackDurationA1;
    [SerializeField] int AttackTickAmountA1;
    [SerializeField] int AtackAmountA1;
    [SerializeField] int LaserAmountA1;
    [SerializeField] int AttackAmountA1;
    [SerializeField] float DelayBetweenAtackA1;

    [SerializeField] List<GameObject> FirepointsA1 = new List<GameObject>();
    List<GameObject> FirePointsFlipedA1 = new List<GameObject>();
    List<LineRenderer> LasersA1 = new List<LineRenderer>();

    //Attack2
    [SerializeField] float CooldownA2;
    [SerializeField] int ProjectileAmountA2;
    [SerializeField] GameObject ProjectilePrefabA2;
    [SerializeField] int BurstAmountA2;
    [SerializeField] float BurstDelayA2;

    bool Finished;
    int OnCooldown;
    float TimeStamp;
    bool Flip;

    // Start is called before the first frame update
    void Start()
    {
        OnCooldown = 2;
        Finished = true;
        SetUpFirepoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if(TimeStamp <= 0 && Finished)
        {
            DecideAttack();
        }

        List<GameObject> FirePointsTemp;
        if (Flip)
        {
            FirePointsTemp = FirePointsFlipedA1;
        }
        else
        {
            FirePointsTemp = FirepointsA1;
        }

        for (int i = 0; i < LasersA1.Count; i++)
        {
            if (LasersA1[i] != null)
            {
                LasersA1[i].SetPosition(0, FirePointsTemp[i].transform.position);
                LasersA1[i].SetPosition(1, FirePointsTemp[i].transform.position + FirePointsTemp[i].transform.up * 100);
            }
        }
    }

    void DecideAttack()
    {
        Finished = false;
        switch(OnCooldown)
        {
            case 2:
                OnCooldown = 1;
                Attack1();
                break;

            case 1:
                OnCooldown = 2;
                Attack2();
                break;
        }
    }

    void Attack1()
    {
        StartCoroutine(Attack1Corutine());
    }

    IEnumerator Attack1Corutine()
    {
        Finished = false;

        for (int j = 0; j < AttackAmountA1; j++)
        {
            float Offset = 360f / LaserAmountA1;
            float OffsetTemp = 0;
            if (Flip)
            {
                OffsetTemp += Offset;
            }

            for (int i = 0; i < LaserAmountA1; i++)
            {
                GameObject Obj = Instantiate(WarningPrefab, FirePoint.position, Quaternion.Euler(0, 0, 0));
                LineRenderer Line = Obj.GetComponent<LineRenderer>();
                LasersA1.Add(Line);
            }

            yield return new WaitForSeconds(WarningDelayA1);

            float TickTime = AttackDurationA1 / AttackTickAmountA1;

            ClearLasers();

            for (int i = 0; i < LaserAmountA1; i++)
            {
                GameObject Obj = Instantiate(LaserPrefab, FirePoint.position, Quaternion.Euler(0, 0, 0));
                LineRenderer Line = Obj.GetComponent<LineRenderer>();
                LasersA1.Add(Line);
            }

            List<GameObject> FirePointsTemp;
            if (Flip)
            {
                FirePointsTemp = FirePointsFlipedA1;
            }
            else
            {
                FirePointsTemp = FirepointsA1;
            }

            for (int i = 0; i < AttackTickAmountA1; i++)
            {
                for (int l = 0; l < LaserAmountA1; l++)
                {
                    RaycastHit2D[] hitInfo = Physics2D.RaycastAll(FirePointsTemp[j].transform.position, FirePointsTemp[j].transform.up);
                    for (int k = 0; k < hitInfo.Length; k++)
                    {
                        if (hitInfo[k].transform.GetComponent<plaerHealth>() != null)
                        {
                            plaerHealth Health = hitInfo[k].transform.GetComponent<plaerHealth>();
                            Health.TakeDamage(DamageA1);
                        }
                    }
                }

                yield return new WaitForSeconds(TickTime);
            }

            if (Flip)
            {
                Flip = false;
            }
            else
            {
                Flip = true;
            }

            ClearLasers();
            yield return new WaitForSeconds(DelayBetweenAtackA1);
        }
        TimeStamp = CoolDownA1;
        Finished = true;
    }

    void Attack2()
    {
        StartCoroutine(Attack2Corutine());
    }

    IEnumerator Attack2Corutine()
    {
        Finished = false;

        for (int i = 0;i < BurstAmountA2;i++)
        {
            float Offset = 360f / LaserAmountA1;
            float OffsetTemp = 0;

            for (int j = 0; j < ProjectileAmountA2; j++)
            {
                Instantiate(ProjectilePrefabA2,FirePoint.position,Quaternion.Euler(0,0,OffsetTemp));
                OffsetTemp += Offset;
            }

            yield return new WaitForSeconds(BurstDelayA2);
        }

        TimeStamp = CooldownA2;
        Finished = true;
    }

    void ClearLasers()
    {
        for (int i = 0; i < LasersA1.Count; i++)
        {
            Destroy(LasersA1[i].gameObject);
        }
        LasersA1.Clear();
    }

    void SetUpFirepoints()
    {
        float Offset = 360f / LaserAmountA1;
        float OffsetTemp = 0;

        GameObject Obj;
        for (int i = 0; i < LaserAmountA1; i++)
        {
            Obj = Instantiate(CubePrefab, FirePoint.position, Quaternion.Euler(0, 0, OffsetTemp));
            Obj.transform.SetParent(transform);
            FirepointsA1.Add(Obj);
            OffsetTemp += Offset;
        }

        OffsetTemp = Offset / 2;
        for (int i = 0; i < LaserAmountA1; i++)
        { 
            Obj = Instantiate(CubePrefab, FirePoint.position, Quaternion.Euler(0, 0, OffsetTemp));
            Obj.transform.SetParent(transform);
            FirePointsFlipedA1.Add(Obj);
            OffsetTemp += Offset;
        }
    }


}
