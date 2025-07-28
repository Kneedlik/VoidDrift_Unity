using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrailObject : MonoBehaviour
{
    List<GameObject> list = new List<GameObject>();
    ParticleSystem part;
    public int Damage;
    public float baseSize;
    float size;

    public float coolDown;
    float timeStamp;

    private void Start()
    {
      part = GetComponent<ParticleSystem>();
      //  TrailSizeScale();
      //  PlayerStats.OnLevel += TrailSizeScale;
    }

    private void Update()
    {
        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }
        else if (timeStamp <= 0)
        {
            dealDamage();
            timeStamp = coolDown;
        }
    }

    void dealDamage()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                list.RemoveAt(i);
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null)
            {
                Health health = list[i].GetComponent<Health>();
                if (health != null)
                {
                    poisonSystem.sharedInstance.Poison(list[i], Damage, ref Damage);
                    health.TakeDamage(Damage);
                }
            }
            list.RemoveAt(i);

        }
    }

    public void TrailSizeScale()
    {
        size = baseSize * (PlayerStats.sharedInstance.areaMultiplier / 100f);
        transform.localScale = new Vector3(transform.localScale.x * size, transform.localScale.y * size, 1);
    }

    private void OnParticleCollision(GameObject other)
    {
       // Debug.Log(1);

        if (other.tag != "Player" && list.Contains(other) == false)
        {
            list.Add(other);
        }
    }

}
