using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianDrone : MonoBehaviour
{
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] GameObject WarningPrefab;
    [SerializeField] GameObject CubePrefab;

    [SerializeField] int Damage;
    [SerializeField] Transform FirePoint;
    [SerializeField] float CoolDown;
    [SerializeField] float AttackDelay;
    [SerializeField] float WarningDelay;
    [SerializeField] float AttackDuration;
    [SerializeField] int AttackTickAmount;
    [SerializeField] int AtackAmount;
    [SerializeField] int LaserAmount;

    [SerializeField] List<GameObject> Firepoints = new List<GameObject>();
    List<GameObject> FirePointsFliped = new List<GameObject>();
    List<LineRenderer> Lasers = new List<LineRenderer>();

    float TimeStamp;
    bool Attacking;
    bool Flip;
    int Index;

    void Start()
    {
        Index = 0;
        SetUpFirepoints();
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if(TimeStamp <=  0)
        {
            if(Attacking == false)
            {
                if (Index == AtackAmount)
                {
                    Attacking = false;
                    TimeStamp = CoolDown;
                    Index = 0;
                }
                else
                {
                    Attacking = true;
                    StartCoroutine(StartAttack());
                    Index++;
                }
            }
        }

        List<GameObject> FirePointsTemp;
        if(Flip)
        {
            FirePointsTemp = FirePointsFliped;
        }else
        {
            FirePointsTemp = Firepoints;
        }

        for (int i = 0; i < Lasers.Count; i++)
        {
            if (Lasers[i] != null)
            {
                Lasers[i].SetPosition(0, FirePointsTemp[i].transform.position);
                Lasers[i].SetPosition(1, FirePointsTemp[i].transform.position + FirePointsTemp[i].transform.up * 1000);
            }
        }
    }

    IEnumerator StartAttack()
    {
        float Offset = 360f / LaserAmount;
        float OffsetTemp = 0;
        if(Flip)
        {
            OffsetTemp += Offset;
        }

        for(int i = 0; i < LaserAmount; i++)
        {
            GameObject Obj = Instantiate(WarningPrefab, FirePoint.position, Quaternion.Euler(0, 0, 0));
            LineRenderer Line = Obj.GetComponent<LineRenderer>();
            Lasers.Add(Line);
        }

        yield return new WaitForSeconds(WarningDelay);

        float TickTime = AttackDuration / AttackTickAmount;

        ClearLasers();

        for(int i = 0;i < LaserAmount; i++)
        {
            GameObject Obj = Instantiate(LaserPrefab, FirePoint.position, Quaternion.Euler(0, 0, 0));
            LineRenderer Line = Obj.GetComponent<LineRenderer>();
            Lasers.Add(Line);
        }

        List<GameObject> FirePointsTemp;
        if (Flip)
        {
            FirePointsTemp = FirePointsFliped;
        }
        else
        {
            FirePointsTemp = Firepoints;
        }

        for (int i = 0; i < AttackTickAmount; i++)
        {
            for (int j = 0; j < LaserAmount; j++)
            {
                RaycastHit2D[] hitInfo = Physics2D.RaycastAll(FirePointsTemp[j].transform.position, FirePointsTemp[j].transform.up);
                for (int k = 0; k < hitInfo.Length; k++)
                {
                    if(hitInfo[k].transform.GetComponent<plaerHealth>() != null)
                    {
                        plaerHealth Health = hitInfo[k].transform.GetComponent<plaerHealth>();
                        Health.TakeDamage(Damage);
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
        Attacking = false;
        TimeStamp = AttackDelay;
    }

    void ClearLasers()
    {
        for (int i = 0; i < Lasers.Count; i++)
        {
            Destroy(Lasers[i].gameObject);
        }
        Lasers.Clear();
    }

    void SetUpFirepoints()
    {
        float Offset = 360f / LaserAmount;
        float OffsetTemp = 0;

        GameObject Obj;
        for (int i = 0; i < LaserAmount; i++)
        {
            Obj = Instantiate(CubePrefab, FirePoint.position, Quaternion.Euler(0, 0, OffsetTemp));
            Obj.transform.SetParent(transform);
            Firepoints.Add(Obj);
            OffsetTemp += Offset;
        }

        OffsetTemp = Offset / 2;
        for (int i = 0; i < LaserAmount; i++)
        {
            Obj = Instantiate(CubePrefab, FirePoint.position, Quaternion.Euler(0, 0, OffsetTemp));
            Obj.transform.SetParent(transform);
            FirePointsFliped.Add(Obj);
            OffsetTemp += Offset;
        }
    }
}
