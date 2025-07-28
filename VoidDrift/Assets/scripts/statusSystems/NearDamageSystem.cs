using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearDamageSystem : MonoBehaviour
{
    public float DamageIncrease;
    public static NearDamageSystem instance;
    [SerializeField] GameObject AuraObj;
    public List<GameObject> AffectedEnems = new List<GameObject>();
    Transform Player;

    void Awake()
    {
        instance = this;
        Player = GameObject.FindWithTag("Player").transform;
    }

    public void SetUp()
    {
        GameObject Obj = Instantiate(AuraObj,Player.transform.position,Quaternion.Euler(0,0,0));
        Obj.transform.SetParent(Player.transform);
    }

    public void AddToList(GameObject Target)
    {
        if(AffectedEnems.Contains(Target) == false)
        {
            Health health = Target.GetComponent<Health>();
            if(health != null)
            {
                AffectedEnems.Add(Target);
                health.multiplier += DamageIncrease;
            }
        }
    }

    public void RemoveFromList(GameObject Target)
    {
        if (AffectedEnems.Contains(Target) != false)
        {
            AffectedEnems.Remove(Target);
            Health health = Target.GetComponent<Health>();
            if (health != null)
            {
                health.multiplier -= DamageIncrease;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
