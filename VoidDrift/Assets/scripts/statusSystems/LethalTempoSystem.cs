using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LethalTempoSystem : MonoBehaviour
{
    public static LethalTempoSystem instance;
    [SerializeField] GameObject Projectile;
    [SerializeField] float DamageCoefitient;
    [SerializeField] int FlatDamage;
    [SerializeField] float AngleOffset;
    public List<GameObject> AffectedTargets = new List<GameObject>();
    Transform Player;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    public void Activation(GameObject target, int damage, ref int plusDamage)
    {
        if(AffectedTargets.Contains(target) == false)
        {
            AffectedTargets.Add(target);
            float Temp = KnedlikLib.ScaleDamage(FlatDamage,true,true);
            Temp = Temp + damage * DamageCoefitient;
            Fire(target.transform,(int)Temp);
        }
    }

    public void Fire(Transform target,int Damage)
    {
        int Flip = Random.Range(0, 1);
        float OffsetTemp;
        if (Flip == 0)
        {
            OffsetTemp = AngleOffset * -1;
        }
        else OffsetTemp = AngleOffset;

        Vector3 Dir = target.transform.position - Player.transform.position;
        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg - 90;
        HomingProjectile Homing = Instantiate(Projectile, Player.position, Quaternion.Euler(0, 0, angle + OffsetTemp)).GetComponent<HomingProjectile>();
        Debug.Log(angle);
        Homing.damage = Damage;
        Homing.damagePlus = Damage;
        Homing.target = target;
    }

    
}
