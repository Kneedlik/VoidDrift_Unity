using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LudensEchoSystem : MonoBehaviour
{
    public static LudensEchoSystem sharedInstance;
    public int damage;
    public int flatDamage;
    public float aoeSize;

    public bool Aoe = false;
    [SerializeField] GameObject explosionObject;
    [SerializeField] float AoeDamageMultiplier;
    [SerializeField] GameObject ImpactEffect;
    public bool burn;
    public int burnAmount;

    public List<GameObject> effectedEnemies = new List<GameObject>();

    //Corrupted
    public List<GameObject> CEffectedEnemies = new List<GameObject>();
    public int CFlatDamage;
    public float CTrueDamage;
    public GameObject CExplosion;
    public GameObject CImpact;

    [SerializeField] float ClearTimer = 1f;
    float Timestamp;

    private void Update()
    {
        if (Timestamp <= 0)
        {
            for (int i = 0; i < effectedEnemies.Count; i++)
            {
                if (effectedEnemies[i] == null)
                {
                    effectedEnemies.RemoveAt(i);
                }
            }

            for (int i = 0; i < CEffectedEnemies.Count; i++)
            {
                if (CEffectedEnemies[i] == null)
                {
                    CEffectedEnemies.RemoveAt(i);
                }
            }

            Timestamp = ClearTimer;
        }
        else
        {
            Timestamp -= Time.deltaTime;
        }
    }

    void Start()
    {
        sharedInstance = this;
    }

    public void CorruptedEchoProc(GameObject target, int Damage, ref int plusDamage)
    {
        if (CEffectedEnemies.Contains(target) == false)
        {
            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                CEffectedEnemies.Add(target);
                int damage = KnedlikLib.ScaleDamage(CFlatDamage, transform, false);
                float pom = health.maxHealth * CTrueDamage;
                damage = damage + (int)pom;

                Color32 c = Constants.CorruptedColor;
                health.TakeDamage(damage,c);

                //explosion EX = Instantiate(CExplosion, target.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<explosion>();
                //EX.damage = damage;
                //EX.TrueDamage = CTrueDamage;

                int Rand = Random.Range(0, 180);
                Instantiate(CImpact, target.transform.position, Quaternion.Euler(0, 0, Rand));
            }

        }   
    }

    public void echoProc(GameObject target, int Damage, ref int plusDamage)
    {
        if(effectedEnemies.Contains(target) == false)
        {
            effectedEnemies.Add(target);

            Health health = target.GetComponent<Health>();
            if (health == null)
            {
                return;
            }

            int pom = Damage;
            float pom2;
            float realDamage = 100 + damage;
            realDamage = (float)realDamage / 100f;
            pom2 = pom * realDamage;
            pom2 += flatDamage;
            pom = (int)pom2;
            pom -= Damage;
            pom = KnedlikLib.ScaleStatusDamage(pom);
            pom2 = pom;

            int Rand = Random.Range(0, 180);
            Instantiate(ImpactEffect,target.transform.position,Quaternion.Euler(0,0,Rand));

            //if (Aoe == false)
            //{
                // health.onDamageEffects(pom);
                // health.damagePopUp(pom);
            Color32 c = new Color32(140, 0, 255, 255);
            health.TakeDamage(pom, c);

            if (burn)
            {
                for (int i = 0; i < burnAmount; i++)
                {
                    int a = 0;
                    SpiritFlameSystem.instance.SpiritFlame(target, 0, ref a);
                }

            }
           
            if(Aoe)
            {
                GameObject E = Instantiate(explosionObject, target.transform.position,Quaternion.Euler(0,0,0));
                explosion ex = E.GetComponent<explosion>();
                pom2 = pom2 * AoeDamageMultiplier;
                pom = (int)pom2;
                ex.damage = pom;
                ex.Colour = new Color32(140, 0, 255, 255);
                ex.IgnoreTargets.Add(target);
                KnedlikLib.ScaleParticleByFloat(E, 1, true);

                if(burn)
                {
                    for (int i = 0; i < burnAmount; i++)
                    {
                        ex.function += SpiritFlameSystem.instance.SpiritFlame;
                    }
                }
            }

          //  pom = (float)pom * realDamage;66
        }
    }

    
}
