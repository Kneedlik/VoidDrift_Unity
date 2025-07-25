using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasersSystem : MonoBehaviour
{
    public static LasersSystem instance;
    [SerializeField] GameObject LaserObj;
    float TimeStamp;

    public int BurstAmount;
    public int BulletsInSalvo;
    public int Damage;
    public float Delay;
    public float CoolDown;

    List<Transform> TargetedEnemies = new List<Transform>();
    bool Ready;
    Transform Target;
    Transform Player;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        enabled = false;
    }

    private void Start()
    {
        Ready = true;
        PlayerStats.OnLevel += Scale;
        Player = GameObject.FindWithTag("Player").transform;
        TimeStamp = CoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if(TimeStamp <= 0 && Ready)
        {
            Ready = false;
            Fire();
        }
    }

    public void Fire()
    {
        StartCoroutine(FireRutine());
    }

    IEnumerator FireRutine()
    {
        TargetedEnemies.Clear();
        for (int i = 0; i < BurstAmount; i++)
        {
            for (int j = 0; j < BulletsInSalvo; j++)
            {
                KnedlikLib.FindRandomEnemy(out Target, TargetedEnemies);
                bool Found = false;

                if (Target != null)
                {
                    Found = true;
                    TargetedEnemies.Add(Target);
                }
                else
                {
                    TargetedEnemies.Clear();
                    KnedlikLib.FindRandomEnemy(out Target);

                    if (Target != null)
                    {
                        TargetedEnemies.Add(Target);
                        Found = true;
                    }
                }

                if (Found)
                {
                    Vector3 pos = (Target.position - Player.position).normalized;

                    GameObject G = Instantiate(LaserObj);

                    LineRenderer line = G.GetComponent<LineRenderer>();
                    line.enabled = true;

                    line.SetPosition(0, Player.position);
                    line.SetPosition(1, Player.position + pos * 100);

                    RaycastHit2D[] hit = Physics2D.RaycastAll(Player.position, pos, 100);
                    for (int k = 0; k < hit.Length; k++)
                    {
                        Health health = hit[k].transform.GetComponent<Health>();
                        if (health != null)
                        {
                            int DamageBase = KnedlikLib.ScaleDamage(Damage, true, true);

                            int DamagePlus = DamageBase;
                            if (eventManager.SummonOnImpact != null)
                            {
                                eventManager.SummonOnImpact(hit[j].transform.gameObject, DamageBase, ref DamagePlus);
                            }

                            if (eventManager.PostImpact != null)
                            {
                                eventManager.PostImpact(hit[j].transform.gameObject, DamagePlus, ref DamagePlus);
                            }

                            int D = 0;
                            BrittleSystem.Instance.ApplyBrittle(hit[j].transform.gameObject, 0, ref D);

                            health.TakeDamage(DamagePlus);
                        }
                    }
                }

                yield return new WaitForSeconds(Delay);
            }
        }
        TimeStamp = CoolDown;
        Ready = true;
    }

    public void Scale()
    {
        if(levelingSystem.instance.level == 15)
        {
            BulletsInSalvo = 2;
            Damage = 20;
        }else if(levelingSystem.instance.level == 30)
        {
            BurstAmount = 30;
            BurstAmount = 4;
            Delay = Delay * 0.6f;
            CoolDown = CoolDown * 0.8f;

        }else if(levelingSystem.instance.level == 50)
        {
            BulletsInSalvo = 3;
            Damage = 50;
            CoolDown = CoolDown * 0.8f;
        }

    }
}
