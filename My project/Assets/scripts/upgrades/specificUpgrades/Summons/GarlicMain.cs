using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicMain : Summon
{
    Transform player;
    public float area;
    float timeStamp;
    bool ready;
    bool pom;
    [SerializeField] GameObject LightningObject;
    [SerializeField] LineRenderer line;
    float radius;

    public bool execute = false;

    void Start()
    {
        scaleSummonDamage();
        scaleSize();

        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        transform.SetParent(player);
        followPosition F = gameObject.GetComponent<followPosition>();
        F.obj = player.gameObject;

        CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
        radius = circle.radius;
        KnedlikLib.DrawCircle(line, radius, 250);
       

        if (GarlicExecute.instance.level > 0)
        {
            execute = true;
        }
        else execute = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pom == false)
        {
            ready = false;
        }

        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if(timeStamp <= 0)
        {
            ready = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(ready)
        {
            pom = false;
            timeStamp = fireRate;

            Health health = collision.GetComponent<Health>();
            if(health != null)
            {
               GameObject G = Instantiate(LightningObject,transform.position,Quaternion.Euler(0,0,0));
                LightningBolt bolt = G.GetComponent<LightningBolt>();

                bolt.StartObject = player.gameObject;
                bolt.EndObject = collision.gameObject;

              //  bolt.StartPosition = player.position;
              //  bolt.EndPosition = collision.transform.position;

                health.TakeDamage(damage);

                if(execute && health != null)
                {
                    float temp = (float)health.health / (float)health.maxHealth;
                    if(temp <= 0.2f)
                    {
                        health.Die();
                    }
                }
            }

        }
    }

    public override void scaleSize()
    {
        size = baseSize * (PlayerStats.sharedInstance.areaMultiplier / 100);
        transform.localScale = new Vector3(size, size, 1);
    }
}
