using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ShotgunLaserDamage : MonoBehaviour
{
    public float DelayBegin;
    public float DelayDamage;
    float Multiplier;
    projectileShotGun ShotGun;
    List<Vector3> PosTemp = new List<Vector3>();
    List<Vector3> OreginTemp = new List<Vector3>();
    List<float> AngleTemp = new List<float>();
    public float Size;
    public int damage;
    Color32 Color;
    // Start is called before the first frame update
    void Start()
    {
        ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        Multiplier = 1f;

        for (int i = 0; i < ShotGun.CubeList.Count; i++)
        {
            Vector3 Temp = new Vector3(0, 0, 0);
            Temp = ShotGun.CubeList[i].transform.up;
            PosTemp.Add(Temp);
            OreginTemp.Add(new Vector3(ShotGun.CubeList[i].transform.position.x, ShotGun.CubeList[i].transform.position.y, ShotGun.CubeList[i].transform.position.x));
            AngleTemp.Add(ShotGun.CubeList[i].transform.rotation.eulerAngles.z);
        }

        for (int i = 0; i < ShotGun.SideCubeList.Count; i++)
        {
            Vector3 Temp = new Vector3(0, 0, 0);
            Temp = ShotGun.SideCubeList[i].transform.up;
            PosTemp.Add(Temp);
            OreginTemp.Add(new Vector3(ShotGun.SideCubeList[i].transform.position.x, ShotGun.SideCubeList[i].transform.position.y, ShotGun.SideCubeList[i].transform.position.z));
            AngleTemp.Add(ShotGun.SideCubeList[i].transform.rotation.eulerAngles.z);
        }

        Invoke("DealDamage", DelayBegin);
        Invoke("DealDamageHalf", DelayBegin + DelayDamage);
        Invoke("DealDamageHalf", DelayBegin + (DelayDamage * 2));
        Invoke("DestroySelf", DelayBegin + (DelayDamage * 3));
    }

    void DealDamage()
    {
        for (int i = 0; i < PosTemp.Count; i++)
        {
            RaycastHit2D[] hitInfo = Physics2D.BoxCastAll(OreginTemp[i], new Vector2(Size * PlayerStats.sharedInstance.areaMultiplier / 100f, Size * PlayerStats.sharedInstance.areaMultiplier / 100f), AngleTemp[i], PosTemp[i].normalized,70f);

            for (int j = 0; j < hitInfo.Length; j++)
            {
                Health health = hitInfo[j].transform.GetComponent<Health>();
                if (health != null)
                {
                    int damagePlus = damage;
                    if (eventManager.ImpactGunOnly != null)
                    {
                        eventManager.ImpactGunOnly(hitInfo[j].transform.gameObject, gameObject);
                    }

                    if (eventManager.OnImpact != null)
                    {
                        eventManager.OnImpact(hitInfo[j].transform.gameObject, damage, ref damagePlus);
                    }

                    if (eventManager.OnCrit != null)
                    {
                        Color32 TempColor = eventManager.OnCrit(hitInfo[j].transform.gameObject, damagePlus, ref damagePlus);
                        Color32 BaseColor = new Color32(0, 0, 0, 0);
                        if (!TempColor.Equals(BaseColor))
                        {
                            Color = TempColor;
                        }
                    }

                    if (eventManager.PostImpact != null)
                    {
                        eventManager.PostImpact(hitInfo[j].transform.gameObject, damagePlus, ref damagePlus);
                    }

                    float damageTemp = damagePlus * Multiplier;
                    damagePlus = (int)damageTemp;

                    if (Color.Equals(new Color32(0, 0, 0, 0)))
                    {
                        health.TakeDamage(damagePlus);
                    }
                    else
                    {
                        health.TakeDamage(damagePlus, Color);
                    }
                }
            }
        }
    }

    void DealDamageHalf()
    {
        Multiplier = 0.5f;
        DealDamage();
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
