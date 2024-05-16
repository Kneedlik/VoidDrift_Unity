using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_BounceFinal : upgrade
{
    [SerializeField] int Bounce;
    [SerializeField] int Damage;
    [SerializeField] float Speed;
    [SerializeField] int ProjectilePenalty;
    [SerializeField] GameObject Explosion;

    public override bool requirmentsMet()
    {
        if(levelingSystem.instance.level >= 40 && AC_Bounce.instance.level >= 2 && levelingSystem.instance.FinallForm == false)
        {
            return true;
        }else return false;
    }

    private void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        AutoCannon Gun = GameObject.FindWithTag("Weapeon").GetComponent<AutoCannon>();

        Gun.baseDamage += Damage;
        Gun.Bounce += Bounce;
        Gun.ForceMultiplier += Speed;
        Gun.BounceDistance = 100;
        PlayerStats.sharedInstance.increaseProjectiles(ProjectilePenalty * -1);
        levelingSystem.instance.FinallForm = true;

        eventManager.ImpactGunOnly += Explode;

        level++;
    }

    public void Explode(GameObject target,GameObject Bullet)
    {
        Instantiate(Explosion,Bullet.transform.position,Bullet.transform.rotation);
    }

}
