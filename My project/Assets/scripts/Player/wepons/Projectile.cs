using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damagePlus;
    public int damage;
    public float destroyTime;
    public int pierce = 0;
    public float knockBack;

    
   
    // Update is called once per frame
    public void setDamage(int Damage)
    {
        damagePlus = Damage;
    }

    public void setArea(float size)
    {
        Transform self = GetComponent<Transform>();
        self.localScale = new Vector3(size, size, 1);
    }

    public void setPierce(int amount)
    {
        pierce = amount;
    }

    public void setKnockBack(float amount)
    {
        knockBack = amount;
    }
}
