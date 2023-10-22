using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSummon : Summon
{
    public int projectiles;
 //   public int chain;
    public float delay;
    float timestamp;
    Transform target;
    List<GameObject> targets = new List<GameObject>();
    int index;

    [SerializeField] GameObject impactParticle;
    

    public bool Aoe;
    [SerializeField] GameObject explosionObject;
    [SerializeField] GameObject lightningBoltObject;
    [SerializeField] GameObject BigLightning;
    

    public bool shock;
    public int shockAmount;
    [SerializeField] GameObject shockObject;
    public float shockSpeed;
   
    void Start()
    {
        fireRate = baseFireRate;
        timestamp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timestamp > 0)
        {
            timestamp -= Time.deltaTime;
        }

        if(timestamp <= 0)
        {
            if (target != null)
            {
                Shoot();

                index++;

                if (index >= projectiles)
                {
                    timestamp = fireRate;
                    index = 0;
                    targets.Clear();
                }else
                {
                    timestamp = delay;
                }
            }
            else if (setRandomTarget(out target) == false)
            {
                target = null;
            }
        }
    }

    public void Shock()
    {
        int offset = Random.Range(0, 90);
        float diff = 180f / shockAmount;
        float pom = diff;

        for (int i = 0; i < shockAmount; i++)
        {
            GameObject G = Instantiate(shockObject, target.position, Quaternion.Euler(0, 0, diff + offset));
            KnedlikLib.ScaleParticleByFloat(G,1f,true);

            Rigidbody2D rb = G.GetComponent<Rigidbody2D>();
            rb.AddForce(G.transform.up * shockSpeed,ForceMode2D.Impulse);
            diff += pom;

            explosion explo = G.GetComponent<explosion>();
            explo.function += ShockImpact;
        }
    }

    public void ShockImpact(GameObject target,int damage,ref int Damage)
    {
        LightningSystem.instance.Shock(target);
    }
    
    public void Shoot()
    {
        scaleSummonDamage();
        scaleSize();

        if(Aoe)
        {
           GameObject E = Instantiate(explosionObject, target.position, Quaternion.Euler(0, 0, 0));
            explosion Explosion = E.GetComponent<explosion>();
            Explosion.destroyTime = 0.8f;
            Explosion.damage = damage;
            KnedlikLib.ScaleParticleByFloat(E, size, true);
            Explosion.function += explosionGraphics;
        }else
        {
            Health health = target.gameObject.GetComponent<Health>();
            health.TakeDamage(damage);
        }

        SkyBeam();
       
    }

    public void explosionGraphics(GameObject Target,int damage,ref int Damage)
    {
        if (target != null)
        {
            GameObject impact = Instantiate(impactParticle, Target.transform.position, Quaternion.Euler(90, 0, 0));
            Vector3 pos = new Vector3(target.position.x, target.position.y, target.position.z);
            impact.transform.SetParent(Target.transform);

            GameObject pom = Instantiate(lightningBoltObject, transform.position, Quaternion.Euler(0, 0, 0));
            LightningBolt bolt = pom.GetComponent<LightningBolt>();
            bolt.StartObject.transform.position = pos;
            bolt.EndObject.transform.position = Target.transform.position;
        }
    }

    public void SkyBeam()
    {
        GameObject pom = Instantiate(BigLightning, transform.position, Quaternion.Euler(0, 0, 0));
        LineRenderer line = pom .GetComponent<LineRenderer>();
        LightningBolt bolt = pom.GetComponent<LightningBolt>();

        line.widthMultiplier = 10f * size;

        bolt.StartObject.transform.position = target.position;
        bolt.EndObject.transform.position = new Vector3(target.position.x, target.position.y + 100, target.position.z);

        pom = Instantiate(impactParticle,target.position,Quaternion.Euler(90,0,0));
        pom.transform.localScale = new Vector3(3,3,3);
        pom.transform.localPosition *= size;
    }

    public override bool setRandomTarget(out Transform target)
    {

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> Enemies2 = new List<GameObject>();
        List<GameObject> Enemies3 = new List<GameObject>();


        Renderer[] renderers = new Renderer[Enemies.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i] = Enemies[i].GetComponent<Renderer>();

            if (renderers[i].isVisible)
            {
                Enemies2.Add(Enemies[i]);
                Enemies3.Add(Enemies[i]);
            }
        }

        if (Enemies2.Count == 0)
        {
            target = Enemies[0].transform;
            return false;
        }
        else
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (Enemies3.Contains(targets[i]))
                {
                    Enemies3.RemoveAt(i);
                }
            }

            if(Enemies3.Count > 0)
            {
                int rand = Random.Range(0, Enemies3.Count);
                target = Enemies3[rand].transform;
                targets.Add(target.gameObject);
            }else
            {
                int rand = Random.Range(0, Enemies2.Count);
                target = Enemies2[rand].transform;
                targets.Clear();
                targets.Add(target.gameObject);
            }

            
        }
        return true;
    }


}
