using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class projectileShotGun : weapeon
{
    public float range;
    public float spread;
    [SerializeField] List<GameObject> CubeList = new List<GameObject>();
    List<GameObject> SideCubeList = new List<GameObject>();

    public GameObject cube;
    public GameObject pellet;

    bool shootCheck = false;
    float timeStamp;

    public int magSize;
    public float reloadSpeed;
    public int currentAmmo;
    float reloadTimeStamp;
    public float BaseOffset;
    public float BaseSideOffset;
    public float MaxSideScaling;
    public float OffsetScaling;
    public float SideOffsetScaling;

    //RingOfFire;
    [HideInInspector] public int RingOfFireCount;
    [HideInInspector] public bool RingOfFireActive;
    Transform Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        SetUpWeapeon();
        timeStamp = 0;

        currentAmmo = magSize;
        setFirepoints();
        setSideFirepoints();
    }

    void Update()
    {
        if (currentAmmo <= 0)
        {
            reloadTimeStamp += Time.deltaTime;
            if (reloadTimeStamp >= reloadSpeed)
            {
                if(RingOfFireActive)
                {
                    RingOfFire();
                }
                currentAmmo = magSize;
                reloadTimeStamp = 0;
            }
        }

        if (timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1"))
        {
            shootCheck = true;
        }
        else
        {
            shootCheck = false;
        }

        if (shootCheck && timeStamp <= 0 && currentAmmo > 0)
        {
            Shoot();
            shootCheck = false;
            timeStamp = CoolDown;
            currentAmmo--;
        }   
    }

    void Shoot()
    {
        Debug.Log("shooting");
        int pom = extraDamage;
        if (eventManager.OnFire != null)
        {
            eventManager.OnFire(gameObject);
        }

        for (int i = 0; i < CubeList.Count; i++)
        {
            GameObject PelletTemp = Instantiate(pellet, transform.position, Quaternion.identity);
            if (eventManager.OnFireAll != null)
            {
                eventManager.OnFireAll(gameObject, PelletTemp);
            }

            PelletTemp.transform.position = firePoint.position;
            Rigidbody2D rb = PelletTemp.GetComponent<Rigidbody2D>(); 
            PelletTemp.transform.rotation = CubeList[i].transform.rotation;

            BulletScript BulletDamage = PelletTemp.GetComponent<BulletScript>();
            BulletDamage.setDamage(damage + extraDamage);
            BulletDamage.setArea(size);
            BulletDamage.setPierce(pierce);
            BulletDamage.setKnockBack(knockBack);

            rb.AddForce(PelletTemp.transform.up * Force, ForceMode2D.Impulse); 
        }

        for (int i = 0; i < SideCubeList.Count; i++)
        {
            GameObject bullet;
            bullet = Instantiate(pellet, SideCubeList[i].transform.position, SideCubeList[i].transform.rotation);

            if (eventManager.OnFireAll != null)
            {
                eventManager.OnFireAll(gameObject, bullet);
            }

            BulletScript BulletDamage = bullet.GetComponent<BulletScript>();
            BulletDamage.setDamage(damage + extraDamage);
            BulletDamage.setArea(size);
            BulletDamage.setPierce(pierce);
            BulletDamage.setKnockBack(knockBack);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(SideCubeList[i].transform.up * Force, ForceMode2D.Impulse);
        }

        extraDamage = pom;
    }

    public override void setFirepoints()
    {
        ResetFirePoints();
        float offset = BaseOffset / projectileCount;
        offset = offset + projectileCount * OffsetScaling;
        float pom = offset;
        int Count = projectileCount;
        bool Switch = false;

        if(projectileCount % 2 == 1)
        {
            GameObject CubeTemp = Instantiate(cube);
            CubeTemp.transform.position = firePoint.position;
            CubeTemp.transform.rotation = Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z);
            CubeTemp.transform.parent = gameObject.transform;
            Count -= 1;
            CubeList.Add(CubeTemp);
        }
        else
        {
            offset = offset / 2;
        }

        for (int i = 0; i < Count; i++)
        {
            GameObject CubeTemp = Instantiate(cube);
            CubeTemp.transform.position = firePoint.position;

            if (Switch)
            {
                CubeTemp.transform.rotation = Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + offset);
                offset += pom;
                Switch = false;
            }else
            {
                CubeTemp.transform.rotation = Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z - offset);
                Switch = true;
            }
            
            CubeTemp.transform.parent = gameObject.transform;
            
            CubeList.Add(CubeTemp);
        }
    }

    public override void setSideFirepoints()
    {
        ResetFirePoints();
        float offset = BaseSideOffset / sideProjectiles;
        offset = offset + (sideProjectiles * SideOffsetScaling);
        if (offset > MaxSideScaling && MaxSideScaling != 0)
        {
            offset = MaxSideScaling;
        }
        float offsetA = offset * 2;

        for (int i = 0; i < sideProjectiles * 2;)
        {
            GameObject CubeTemp;
            CubeTemp = Instantiate(cube, firePoint.position, Quaternion.Euler(0, 0, (offsetA * -1) + 270));
            CubeTemp.transform.parent = gameObject.transform;
            SideCubeList.Add(CubeTemp);
            i++;
            CubeTemp = Instantiate(cube, firePoint.position, Quaternion.Euler(0, 0, offsetA + 270));
            CubeTemp.transform.parent = gameObject.transform;
            SideCubeList.Add(CubeTemp);
            i++;
            offsetA += offset;
        }
    }

    public override void ResetFirePoints()
    {
        for (int i = 0; i < CubeList.Count; i++)
        {
            Destroy(CubeList[i]);
        }
        CubeList.Clear();
    }

    public void ResetSideFirePoints()
    {
        for (int i = 0; i < SideCubeList.Count; i++)
        {
            Destroy(SideCubeList[i]);
        }
        SideCubeList.Clear();
    }

    public override GameObject GetProjectile()
    {
        return pellet;
    }

    public void RingOfFire()
    {
        float offset = Player.rotation.eulerAngles.z;
        float pom = 360 / RingOfFireCount;

        for(int i = 0;i < RingOfFireCount ;i++)
        {
            GameObject Pellet = Instantiate(pellet, Player.position, Quaternion.Euler(0, 0, offset));
            Rigidbody2D rigidbody2D = Pellet.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(Pellet.transform.up * Force * 0.75f);
            offset += pom;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
