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
    [SerializeField] GameObject ImpactEffect;
    public bool burn;
    public int burnAmount;

    public List<GameObject> effectedEnemies = new List<GameObject>();
    void Start()
    {
        sharedInstance = this;
    }

    public void echoProc(GameObject target, int Damage, ref int plusDamage)
    {
        if(effectedEnemies.Contains(target) == false)
        {
            effectedEnemies.Add(target);

            int pom = Damage;
            float pom2;
            float realDamage = 100 + damage;
            realDamage = (float)realDamage / 100f;
            pom += flatDamage;
            pom2 = pom * realDamage;
            pom = (int)pom2;
            pom -= Damage;

            Instantiate(ImpactEffect,transform.position,Quaternion.Euler(0,0,0));

            if (Aoe == false)
            {
                Health health = target.GetComponent<Health>();
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
            }else if(Aoe)
            {
               GameObject E = Instantiate(explosionObject, target.transform.position,Quaternion.Euler(0,0,0));
                explosion ex = E.GetComponent<explosion>();
                ex.damage = pom;
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
