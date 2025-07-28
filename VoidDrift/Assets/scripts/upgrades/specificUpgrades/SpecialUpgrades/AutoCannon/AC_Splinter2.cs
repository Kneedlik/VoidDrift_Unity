using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Splinter2 : upgrade
{
    [SerializeField] GameObject Prefab;
    public int Amnount;
    public float BaseOffset;
    public float Force;
    public float Multiplier;

    public float SpeedMultiplier;
    public Sprite MainSprite;
    public Material material;

    [SerializeField] GameObject Smoke;
    [SerializeField] GameObject Sparks;
    [SerializeField] GameObject Explosion;
    

    void Start()
    {
        Type = type.special;
        setColor();
    }

    public override void function()
    {
        if (level == 0)
        {
            eventManager.ImpactGunOnly += SplinterFunc;
            description = string.Format("Shrapnels + 2");
        }else
        {
            Amnount += 2;
        }

        level++;
    }

    public void SplinterFunc(GameObject target, GameObject bullet)
    {
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        AutoCannon autoCanon = GameObject.FindWithTag("Weapeon").GetComponent<AutoCannon>();
        SpriteRenderer sRenderer = bulletScript.GetComponent<SpriteRenderer>();

        if (bulletScript.pierce == autoCanon.pierce && bulletScript.Bounce == autoCanon.pierce)
        {

            int Count = 0;
            float Offset = BaseOffset / Amnount;
            Offset = Offset + (Amnount * Multiplier);
            float OffsetTemp = Offset;
            Rigidbody2D rb;
            GameObject Obj;

            Obj = Instantiate(Sparks, bullet.transform.position, Quaternion.Euler(bullet.transform.rotation.eulerAngles.z - 90,-90,0));
            KnedlikLib.ScaleParticleByFloat(Obj,1,true);
            Obj = Instantiate(Smoke, bullet.transform.position, Quaternion.Euler(bullet.transform.rotation.eulerAngles.z - 90, -90, 0));
            KnedlikLib.ScaleParticleByFloat(Obj, 1, true);

            if(Explosion != null)
            {
                Obj = Instantiate(Explosion, bullet.transform.position, Quaternion.Euler(bullet.transform.rotation.eulerAngles.z - 90, -90, 0));
                KnedlikLib.ScaleParticleByFloat(Obj, 1, true);
            }

            if (Amnount % 2 == 1)
            {
                Count++;
                Obj = Instantiate(Prefab, bullet.transform.position, bullet.transform.rotation);
                rb = Obj.GetComponent<Rigidbody2D>();
                rb.AddForce(Obj.transform.up * Force, ForceMode2D.Impulse);

            }
            else
            {
                Obj = Instantiate(Prefab, bullet.transform.position, Quaternion.Euler(0, 0, bullet.transform.rotation.eulerAngles.z + (Offset / 2)));
                rb = Obj.GetComponent<Rigidbody2D>();
                rb.AddForce(Obj.transform.up * Force, ForceMode2D.Impulse);

                Obj = Instantiate(Prefab, bullet.transform.position, Quaternion.Euler(0, 0, bullet.transform.rotation.eulerAngles.z - (Offset / 2)));
                rb = Obj.GetComponent<Rigidbody2D>();
                rb.AddForce(Obj.transform.up * Force, ForceMode2D.Impulse);

                Count += 2;
                OffsetTemp = Offset / 2;
            }

            for (int i = 0; i < (Amnount - Count) / 2; i++)
            {
                Obj = Instantiate(Prefab, bullet.transform.position, Quaternion.Euler(0, 0, bullet.transform.rotation.eulerAngles.z + OffsetTemp));
                rb = Obj.GetComponent<Rigidbody2D>();
                rb.AddForce(Obj.transform.up * Force, ForceMode2D.Impulse);

                Obj = Instantiate(Prefab, bullet.transform.position, Quaternion.Euler(0, 0, bullet.transform.rotation.eulerAngles.z - OffsetTemp));
                rb = Obj.GetComponent<Rigidbody2D>();
                rb.AddForce(Obj.transform.up * Force, ForceMode2D.Impulse);

                //Debug.Log(1);

                OffsetTemp += Offset;

            }

            rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = rb.velocity * SpeedMultiplier;

            sRenderer.sprite = MainSprite;
            sRenderer.material = material;

        }
    }

    public void ScaleStuff(GameObject Obj)
    {
        BaseProjectile projectile = Obj.GetComponent<BaseProjectile>();
        KnedlikLib.ScaleDamage(projectile.Damage, true, true);
        KnedlikLib.ScaleParticleByFloat(Obj, 1, true);
        projectile.destroyTime = projectile.destroyTime * (PlayerStats.sharedInstance.areaMultiplier / 100f);
    }
}
