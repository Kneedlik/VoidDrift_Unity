using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public int damage;
    public float fireRate;
    public float size = 1;

    public int baseDamage;
    public float baseFireRate;
    public float baseSize = 1;

    public int id;

    public class MyMath
    {
        public static int solveQuadratic(float a, float b, float c, out float x1, out float x2)
        {
            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                x1 = Mathf.Infinity;
                x2 = -x1;
                return 0;
            }
            x1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
            x2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
            return discriminant > 0 ? 2 : 1;

        }
    }

    public bool InterceptionPoint(Vector2 a, Vector2 b, Vector2 va, float speedB, out Vector2 result)
    {
        var aToB = b - a;
        var dc = aToB.magnitude;
        var alpha = Vector2.Angle(aToB, va) * Mathf.Deg2Rad;
        var speedA = va.magnitude;
        var r = speedA / speedB;
        if (MyMath.solveQuadratic(1 - r * r, 2 * r * dc * Mathf.Cos(alpha), -(dc * dc), out var x1, out var x2) == 0)
        {
            result = Vector2.zero;
            return false;
        }
        var da = Mathf.Max(x1, x2);
        var t = da / speedB;
        Vector2 c = a + va * t;

        result = (c - b).normalized;
        return true;
    }

   public bool setClosestTarget(out Transform target)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
       // Transform[] Transforms = new Transform[Enemies.Length];
        Renderer[] renderers = new Renderer[Enemies.Length];

        for (int i = 0; i < Enemies.Length; i++)
        {
            //Transforms[i] = Enemies[i].GetComponent<Transform>();
            renderers[i] = Enemies[i].GetComponent<Renderer>();
        }

        target = Enemies[0].transform;
       int j = 0;
       

        for (int i = 0; i < Enemies.Length; i++)
        {
            if (renderers[i].isVisible)
            {
                if (Vector3.Distance(Enemies[i].transform.position,player.position) < Vector3.Distance(target.position,player.position))
                {
                    target = Enemies[i].transform;
                    j = i;
                }
            }
        }

        if (renderers[j].isVisible)
        {
            return true;
        }
        else return false; 
    }

    public virtual bool setRandomTarget(out Transform target)
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> Enemies2 = new List<GameObject>();

        Renderer[] renderers = new Renderer[Enemies.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i] = Enemies[i].GetComponent<Renderer>();

            if (renderers[i].isVisible)
            {
                Enemies2.Add(Enemies[i]);
            }
        }

        if(Enemies2.Count == 0)
        {
            target = Enemies[0].transform;
            return false;
        }else
        {
            int rand = Random.Range(0,Enemies2.Count);
            target = Enemies2[rand].transform;
        }
        return true;
    }

    public virtual void scaleSummonDamage()
    {
        float pom = baseDamage * (PlayerStats.sharedInstance.SummonDamage / 100f);
        pom = pom * (PlayerStats.sharedInstance.damageMultiplier / 100f);
        pom += PlayerStats.sharedInstance.ExtraDamage;
        pom = pom * MasterManager.Instance.PlayerInformation.SummonDamageMultiplier;
        pom = pom * MasterManager.Instance.PlayerInformation.DamageMultiplier;
        damage = (int)pom;
    }

    public void faceEnemy(Transform target)
    {
        if(transform.position.x - target.position.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public virtual void scaleSize()
    {
        size = baseSize * (PlayerStats.sharedInstance.areaMultiplier / 100f);
        size = size * MasterManager.Instance.PlayerInformation.SizeMultiplier;
    }

    public virtual void scaleFireRate()
    {
        fireRate = baseFireRate;
    }

    public virtual void scaleForce()
    {

    }

    public void ScaleSummonStats()
    {
        scaleSummonDamage();
        scaleSize();
        scaleFireRate();
        scaleForce();
    }

    public virtual int PrintPowerLevel()
    {
        float PowerLevel = baseDamage / baseFireRate;
        //Debug.Log(string.Format("Power level: {0}",(int)PowerLevel));
        return (int)PowerLevel;
    }
}
