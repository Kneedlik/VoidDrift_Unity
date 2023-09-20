using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public static class KnedlikLib 
{

    public static void scaleParticleSize(GameObject target, GameObject particle, float multiplier)
    {
        float size;
        Vector3 pom;

        if (target != null)
        {
            SpriteRenderer S = target.GetComponent<SpriteRenderer>();
            

            Collider2D C = target.GetComponent<Collider2D>();
            pom = S.bounds.size;
            Debug.Log(pom);
            size = pom.x * pom.y;

            float temp = particle.transform.localScale.x;
            size *= multiplier / 15;

            Debug.Log(size);

            if(size > temp * 4)
            {
                size = temp * 3;
            }else if(size < temp)
            {
                size = temp;
            }

            
            particle.transform.localScale = new Vector3(size, size, size);

            foreach (Transform item in particle.transform)
            {
                ParticleSystem P = item.GetComponent<ParticleSystem>();
                Light2D L = item.GetComponent<Light2D>();
                if(P != null)
                {
                    item.localScale = new Vector3(size,size,size);
                }

                if(L != null)
                {
                    L.pointLightOuterRadius *= size;
                }
            }

        }
    }

    public static void ScaleParticleByFloat(GameObject target,float multiplier,bool areaMultiplier)
    {
        float size = multiplier;

        if(areaMultiplier)
        {
            size = size * (PlayerStats.sharedInstance.areaMultiplier / 100f);
        }

        target.transform.localScale = new Vector3(size, size, size);

        foreach (Transform item in target.transform)
        {
            ParticleSystem P = item.GetComponent<ParticleSystem>();
            Light2D L = item.GetComponent<Light2D>();

            if (P != null)
            {
                item.localScale = new Vector3(size, size, size);
            }

            if (L != null)
            {
                L.pointLightOuterRadius *= size;
            }
        }
    }

    public static int ScaleDamage(int damage,bool multiplier,bool extra)
    {
        float Damage = damage;

        if(multiplier)
        {
            Damage = Damage * (PlayerStats.sharedInstance.damageMultiplier / 100f);
        }

        if(extra)
        {
            Damage += PlayerStats.sharedInstance.ExtraDamage;
        }

        int pom = (int)Damage;

        return pom;
    }

    public static void DrawCircle(LineRenderer line,float radius,int steps)
    {
        line.positionCount = steps;

        for (int i = 0; i < steps; i++)
        {
            float progress = (float)i / steps;
            float radian = progress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(radian);
            float yScaled = Mathf.Sin(radian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 pos = new Vector3(x, y, 0);
            line.SetPosition(i, pos);
        }
    }

    public static float ReturtAngle(Vector3 target,Vector3 self)
    {
        Vector3 lookDir = (target - self).normalized;
        float angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
        return angle;
    }

    public static void lookAt2d(Transform self,Transform target,float offset)
    {
        self.LookAt(target);
        self.rotation = Quaternion.Euler(0, 0, self.rotation.eulerAngles.y < 180 ? 270 - self.rotation.eulerAngles.x : self.rotation.eulerAngles.x - offset);
    }

    public static bool FindClosestEnemy(Transform self,out Transform target)
    {

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
                if (Vector3.Distance(Enemies[i].transform.position, self.position) < Vector3.Distance(target.position, self.position))
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

   public static bool FindClosestEnemy(Transform self,out Transform target, List<Transform> list)
   {

        target = null;
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Renderer[] renderers = new Renderer[Enemies.Length];
        List<GameObject> Enemies2 = new List<GameObject>();

        for (int i = 0; i < Enemies.Length; i++)
        {
            renderers[i] = Enemies[i].GetComponent<Renderer>();
        }

        for (int i = 0; i < Enemies.Length; i++)
        {
            if (renderers[i].isVisible && list.Contains(Enemies[i].transform) == false)
            {
                Enemies2.Add(Enemies[i]);
            }
        }

        if(Enemies2.Count > 0)
        {
            for (int i = 0; i < Enemies2.Count; i++)
            {
                if(target == null)
                {
                    target = Enemies2[i].transform;
                }else if (Vector3.Distance(target.position, self.position) > Vector3.Distance(Enemies2[i].transform.position,self.position))
                {
                    target = Enemies2[i].transform;
                }
            }

            return true;

        }else
        {
            return false;
        }
    }

    public static bool FindClosestEnemy(Transform self, out Transform target, List<Transform> list, bool TargetEnviroment)
    {
        target = null;
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
       
        List<Renderer> renderers = new List<Renderer>();
        List<GameObject> Enemies2 = new List<GameObject>();

        for (int i = 0; i < Enemies.Length; i++)
        {
            renderers.Add(Enemies[i].GetComponent<Renderer>()); 
        }

        if(TargetEnviroment)
        {
            GameObject[] Enviroment = GameObject.FindGameObjectsWithTag("Enviroment");

            for (int i = 0; i < Enviroment.Length; i++)
            {
                renderers.Add(Enviroment[i].GetComponent<Renderer>());
            }
        }
        
        for (int i = 0; i < renderers.Count; i++)
        {
            if (renderers[i].isVisible && list.Contains(Enemies[i].transform) == false)
            {
                Enemies2.Add(Enemies[i]);
            }
        }

        if (Enemies2.Count > 0)
        {
            for (int i = 0; i < Enemies2.Count; i++)
            {
                if (target == null)
                {
                    target = Enemies2[i].transform;
                }
                else if (Vector3.Distance(target.position, self.position) > Vector3.Distance(Enemies2[i].transform.position, self.position))
                {
                    target = Enemies2[i].transform;
                }
            }

            return true;

        }
        else
        {
            return false;
        }
    }

    public static bool FindRandomEnemy(out Transform target)
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

        if (Enemies2.Count == 0)
        {
            target = Enemies[0].transform;
            return false;
        }
        else
        {
            int rand = Random.Range(0, Enemies2.Count);
            target = Enemies2[rand].transform;
        }
        return true;
    }

    public static int GetPercencHP(GameObject target, float percent)
    {
        Health health = target.GetComponent<Health>();
        float pom =  (float)health.maxHealth * percent;

        return (int)pom;
    }

    public static int ScaleByLevel(int health)
    {
        float pom = (float)health * levelingSystem.instance.healthPerLevel * (levelingSystem.instance.level - 1);
        pom += health;
        return (int)pom;
    }

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

    public static bool InterceptionPoint(Vector2 target, Vector2 FirePoint, Vector2 TargetVelocity, float speedB, out Vector2 result)
    {
        var aToB = FirePoint - target;
        var dc = aToB.magnitude;
        var alpha = Vector2.Angle(aToB, TargetVelocity) * Mathf.Deg2Rad;
        var speedA = TargetVelocity.magnitude;
        var r = speedA / speedB;
        if (MyMath.solveQuadratic(1 - r * r, 2 * r * dc * Mathf.Cos(alpha), -(dc * dc), out var x1, out var x2) == 0)
        {
            result = Vector2.zero;
            return false;
        }
        var da = Mathf.Max(x1, x2);
        var t = da / speedB;
        Vector2 c = target + TargetVelocity * t;

        result = (c - FirePoint).normalized;
        return true;
    }

    public static bool InterceptionPoint(Vector2 target, Vector2 FirePoint, Vector2 TargetVelocity, float speedB, out Vector2 result, out float resultAngle)
    {
        var aToB = FirePoint - target;
        var dc = aToB.magnitude;
        var alpha = Vector2.Angle(aToB, TargetVelocity) * Mathf.Deg2Rad;
        var speedA = TargetVelocity.magnitude;
        var r = speedA / speedB;
        if (MyMath.solveQuadratic(1 - r * r, 2 * r * dc * Mathf.Cos(alpha), -(dc * dc), out var x1, out var x2) == 0)
        {
            result = Vector2.zero;
            resultAngle = 0;
            return false;
        }
        var da = Mathf.Max(x1, x2);
        var t = da / speedB;
        Vector2 c = target + TargetVelocity * t;

        result = (c - FirePoint).normalized;
        resultAngle = Mathf.Atan2(result.y, result.x) * Mathf.Rad2Deg - 90;
        return true;
    }

    public static void SetMaxSpeed(float MaxSpeed, Rigidbody2D rb)
    {
        if (rb != null)
        {
            if (rb.velocity.magnitude > MaxSpeed)
            {
                rb.velocity = rb.velocity.normalized * MaxSpeed;
            }
        }
    }


}
