using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Explode : upgrade
{
    float AreaIncrease;
    int DamageIncrease;
    int Damage;
    float AreaMultiplier = 1;
    [SerializeField] GameObject ExplosionObj;
    // Start is called before the first frame update
    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        if (level == 0)
        {
            eventManager.ImpactGunOnly += Explode;
            description = string.Format("Bullet explosion base Damage + {0} Base Area + {1}% ",DamageIncrease,AreaIncrease * 10);
        }else if (level == 1)
        {
            Damage += DamageIncrease;
            AreaMultiplier += 0.1f;
        }
        level++;
    }

    public void Explode(GameObject target, GameObject Bullet)
    {
        GameObject ObjTemp = Instantiate(ExplosionObj, Bullet.transform.position, Bullet.transform.rotation);
        explosion Explo = ObjTemp.GetComponent<explosion>();
        Explo.damage = Damage;
        KnedlikLib.ScaleParticleByFloat(ObjTemp,AreaMultiplier,false);
    }
}
