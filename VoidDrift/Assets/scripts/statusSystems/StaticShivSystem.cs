using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShivSystem : MonoBehaviour
{
    public static StaticShivSystem instance;
    public GameObject BoltObject;
    public GameObject impactEffect;
    public float Charge;
    public float ChargeNeeded;
    public int BaseDamage;
    public int Damage;
    public float Delay;

    Rigidbody2D Rb;
    Transform Player;
    Transform target;
    Transform CurrentTarget;
    public List<Transform> List = new List<Transform>();
    public float maxDistance;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Rb = Player.GetComponent<Rigidbody2D>();
        instance = this;    
    }


    void Update()
    {
        if(StaticShiv.Instance.level > 0)
        {
        Charge += Rb.velocity.magnitude * Time.deltaTime;
        }
    }

    public void StaticShivTrigger(GameObject weapeon)
    {
        StartCoroutine(StaticShivRoutine());
    }

    IEnumerator StaticShivRoutine()
    {
        if (Charge >= ChargeNeeded)
        {
            float temp = Charge / ChargeNeeded;
            temp = temp * (PlayerStats.sharedInstance.damageMultiplier / 100f);
            temp = BaseDamage * temp;
            temp += PlayerStats.sharedInstance.ExtraDamage;
            Damage = (int)temp;
                
            List.Clear();
            if (KnedlikLib.FindClosestEnemy(Player, out target, List,true))
            {
                GameObject L = Instantiate(BoltObject);
                LightningBolt Bolt = L.GetComponent<LightningBolt>();

                Bolt.StartObject = Player.gameObject;
                Bolt.EndObject = target.gameObject;

                Health health = target.GetComponent<Health>();
                health.TakeDamage(Damage);

                Instantiate(impactEffect, Player.position, Quaternion.Euler(0, 0, 0));
                Instantiate(impactEffect, target.position, Quaternion.Euler(0, 0, 0));

                CurrentTarget = target;
                List.Add(target);
                yield return new WaitForSeconds(Delay);

                while (KnedlikLib.FindClosestEnemy(CurrentTarget, out target,List,true))
                {
                    if (Vector3.Distance(CurrentTarget.position, target.position) < maxDistance)
                    {
                        Instantiate(impactEffect, target.position, Quaternion.Euler(0, 0, 0));

                        L = Instantiate(BoltObject);
                        Bolt = L.GetComponent<LightningBolt>();

                        Bolt.StartObject = CurrentTarget.gameObject;
                        Bolt.EndObject = target.gameObject;

                        Instantiate(impactEffect, target.position, Quaternion.Euler(0, 0, 0));

                        health = target.GetComponent<Health>();
                        health.TakeDamage(Damage);

                        CurrentTarget = target;
                        List.Add(target);

                        yield return new WaitForSeconds(Delay);

                    }
                    else
                    {
                        Charge = 0;
                        break;
                    }
                }

            }
        }

        Charge = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
