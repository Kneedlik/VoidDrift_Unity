using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSummonMain :Summon
{
    [SerializeField] GameObject orb;
    [SerializeField] List<Transform> Points = new List<Transform>();
    [SerializeField] List<Transform> Points2 = new List<Transform>();
    [SerializeField] List<SpininigSummonOrb> orbs = new List<SpininigSummonOrb>();
    [SerializeField] int orbCount = 3;
    public float orbDistance;
    [SerializeField] List<float> points = new List<float>();

    void Start()
    {
        for (int i = 0; i < orbCount; i++)
        {
            if (orb != null && orbCount == 5)
            {
               GameObject e = Instantiate(orb, Points2[i].position, Quaternion.Euler(0, 0, 0));
                followPosition f = e.GetComponent<followPosition>();
                f.obj = Points2[i].gameObject;
                orbs.Add(e.GetComponent<SpininigSummonOrb>());

            }else
            {
                GameObject e = Instantiate(orb, Points[i].position, Quaternion.Euler(0, 0, 0));
                followPosition f = e.GetComponent<followPosition>();
                f.obj = Points[i].gameObject;
                orbs.Add(e.GetComponent<SpininigSummonOrb>());
            }
        }

        setDamage();
      //  increaseOrbCount(2);
        setDistance();
      //  Invoke("scaleSize", 3);
      scaleSize();
    }

    private void Update()
    {
        int index = 0;

        for (int i = 0; i < orbCount; i++)
        {
            index++;
            if(index > orbCount - 1)
            {
                index = 0;
            }

            points[i] = Vector3.Distance(Points[index].position, Points[i].position);
            
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position,orbDistance);
    }

    public void setDistance()
    {
        
        transform.localScale = new Vector3(orbDistance, orbDistance, 1);
       // increaseOrbCount(0);
    }
   
    public void setDamage()
    {
        for (int i = 0; i < orbCount; i++)
        {
            if (orbs[i] != null)
            {
                orbs[i].damage = damage;
            }
        }
    }

    public void setSpeed()
    {
        Orbiting orbiting = gameObject.GetComponent<Orbiting>();

        orbiting.speed = fireRate;
    }

    public void increaseOrbCount(int amount)
    {
        int pom = orbCount;
        orbCount += amount;

        transform.rotation = Quaternion.Euler(0, 0, 0);

        if(orbCount == 5)
        {
            for (int i = 0; i < pom; i++)
            {
                followPosition f = orbs[i].GetComponent<followPosition>();
                f.obj = Points2[i].gameObject;
            }

            for (int i = 0; i < amount; i++)
            {
                GameObject e = Instantiate(orb, Points2[i + pom].position, Quaternion.Euler(0, 0, 0));
                followPosition f = e.GetComponent<followPosition>();
                f.obj = Points2[i + pom].gameObject;
                orbs.Add(e.GetComponent<SpininigSummonOrb>());
            }
        }else
        {
            for (int i = 0; i < pom; i++)
            {
                followPosition f = orbs[i].GetComponent<followPosition>();
                f.obj = Points[i].gameObject;
            }


            for (int i = 0; i < amount; i++)
            {
                GameObject e = Instantiate(orb, Points[i + pom].position, Quaternion.Euler(0, 0, 0));
                followPosition f = e.GetComponent<followPosition>();
                f.obj = Points[i + pom].gameObject;
                orbs.Add(e.GetComponent<SpininigSummonOrb>());
            }
        }
    }

    public override void scaleSize()
    {
        size = baseSize * (PlayerStats.sharedInstance.areaMultiplier / 100);

        for (int i = 0; i < orbs.Count; i++)
        {
            TrailRenderer trail = orbs[i].GetComponent<TrailRenderer>();
            float pom = size * 0.8f;
            trail.startWidth = pom;

            orbs[i].transform.localScale = new Vector3(size, size, 1);

        }
    }

    

    
}
