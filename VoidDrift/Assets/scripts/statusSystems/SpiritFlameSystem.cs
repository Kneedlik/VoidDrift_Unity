using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class SpiritFlameSystem : MonoBehaviour
{
    public static SpiritFlameSystem instance;

    public int damage;
    public float TrueDamage;
    public int maxStacks;
    public float duration;
    public float speed;

   [SerializeField] List<GameObject> enemies = new List<GameObject> ();
   [SerializeField] List<int> stacks = new List<int> ();
   [SerializeField] List<float> timers = new List<float>();

    [SerializeField] Material purple;
    [SerializeField] new GameObject light;

    [SerializeField] GameObject Fire;
   
    public bool active;

    float Timestamp;
    [SerializeField] float ListClearTimer = 3f;

    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Timestamp <= 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    stacks.RemoveAt(i);
                    timers.RemoveAt(i);
                }
            }

            Timestamp = ListClearTimer;
        }
        else
        {
            Timestamp -= Time.deltaTime;
        }
    }

    public void SpiritFlame(GameObject target,int damage, ref int Damage)
    {
       // for (int i = 0; i < enemies.Count; i++)
       // {
        //    if (enemies[i] == null)
        //    {
        //        enemies.RemoveAt (i);
        //       stacks.RemoveAt(i);
        //        timers.RemoveAt(i);
        //    }
       // }

        if (active)
        {
            if (enemies.Contains(target))
            {
                int i = enemies.IndexOf(target);
                if (stacks[i] < maxStacks)
                {
                    stacks[i]++;
                }
                timers[i] = duration;
            } else if(enemies.Contains(target) == false)
            {
                bool full = true;

                for (int j = 0; j < enemies.Count; j++)
                {
                    if (enemies[j] == null && timers[j] <= 0)
                    {
                        enemies[j] = target;
                        stacks[j] = 1;
                        timers[j] = duration;
                        full = false;
                        break;
                    }
                }

                if(full)
                {
                    enemies.Add(target);
                    stacks.Add(1);
                    timers.Add(duration);
                }
                
                int i = enemies.IndexOf(target);

                GameObject F = Instantiate(Fire, target.transform.position, Quaternion.Euler(0, 0, 0));
                //KnedlikLib.scaleParticleSize(target, F, 1);
                F.transform.SetParent(target.transform);

                StartCoroutine(startSpiritFlame(target));
                StartCoroutine(endSpiritFlame(target,F));
               
            }  
        }   
    }

    public void ChangeColor(GameObject weapeon,GameObject bullet)
    {
        if (active)
        {
            SpriteRenderer S = bullet.GetComponent<SpriteRenderer>();
            S.material = purple;

            foreach(Transform t in bullet.transform)
            {
                Light2D L = t.GetComponent<Light2D>();
                if(L != null)
                {
                   
                    Destroy(t.gameObject);
                    break;
                }
            }

            GameObject p = Instantiate(light, bullet.transform.position, bullet.transform.rotation);
            p.transform.SetParent(bullet.transform);
        }
    }

    IEnumerator startSpiritFlame(GameObject target)
    {
        yield return new WaitForSeconds(speed);

        Health health = null;

        if (target != null)
        {
            health = target.GetComponent<Health>();
        }
        
        while (enemies.Contains (target) && target != null && target.activeSelf)
        {
            if(health != null)
            {
                int index = enemies.IndexOf(target);
                int Damage = damage * stacks[index];
                float pom = (TrueDamage * stacks[index]) / 100f;
                pom = health.maxHealth * pom;
                Damage = KnedlikLib.ScaleDamage(Damage, true, true);
                Damage = KnedlikLib.ScaleStatusDamage(Damage);
                Damage = Damage + (int)pom;
                health.TakeDamage(Damage);
            }

            yield return new WaitForSeconds(speed * PlayerStats.sharedInstance.TickRate);
        }
    }

    

    IEnumerator endSpiritFlame(GameObject Enemy,GameObject Vfx)
    {
        if (Enemy != null)
        {
            if(enemies.Contains(Enemy))
            {
                while (Enemy != null && enemies.Contains(Enemy) &&  timers[enemies.IndexOf(Enemy)] > 0)
                {
                    timers[enemies.IndexOf(Enemy)] -= Time.deltaTime;
                    yield return new WaitForSeconds(0);
                }

                if (enemies.Contains(Enemy) && Enemy != null)
                {
                    stacks[enemies.IndexOf(Enemy)] = 0;
                    enemies[enemies.IndexOf(Enemy)] = null; //
                }
            }
        }

        if(Vfx != null)
        {
            Destroy(Vfx);
        }
    }
}
