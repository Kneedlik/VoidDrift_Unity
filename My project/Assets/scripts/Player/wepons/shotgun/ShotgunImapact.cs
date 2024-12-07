using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunImapact : MonoBehaviour
{
    [HideInInspector] public float Delay;
    [SerializeField] GameObject ImpactObj;
    List<GameObject> IgnoreObj = new List<GameObject>();
    List<Vector3> PosTemp = new List<Vector3>();
    List<Vector3> OreginTemp = new List<Vector3>();
    List<float> AngleTemp = new List<float>();
    projectileShotGun ShotGun;
    public float Size;
    public bool Cone;
    Transform Player;
    void Start()
    {
        ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        if (Cone)
        {
            float offset = 0;
            float pom = 360 / ShotGun.RingOfFireCount;

            for (int i = 0; i < ShotGun.RingOfFireCount; i++)
            {
                PosTemp.Add(new Vector3(Mathf.Sin(offset * Mathf.Deg2Rad), Mathf.Cos(offset * Mathf.Deg2Rad), 0));
                OreginTemp.Add(Player.position);
                AngleTemp.Add(offset);
                offset += pom;
            }
        }
        else
        {
            for (int i = 0; i < ShotGun.CubeList.Count; i++)
            {
                Vector3 Temp = new Vector3(0, 0, 0);
                Temp = ShotGun.CubeList[i].transform.up;
                PosTemp.Add(Temp);
                OreginTemp.Add(new Vector3(ShotGun.CubeList[i].transform.position.x, ShotGun.CubeList[i].transform.position.y, 0));
                AngleTemp.Add(ShotGun.CubeList[i].transform.rotation.eulerAngles.z);
            }

            for (int i = 0; i < ShotGun.SideCubeList.Count; i++)
            {
                Vector3 Temp = new Vector3(0, 0, 0);
                Temp = ShotGun.SideCubeList[i].transform.up;
                PosTemp.Add(Temp);
                OreginTemp.Add(new Vector3(ShotGun.SideCubeList[i].transform.position.x, ShotGun.SideCubeList[i].transform.position.y, 0));
                AngleTemp.Add(ShotGun.SideCubeList[i].transform.rotation.eulerAngles.z);
            }
        }

        SpawnLine();
        Invoke("SpawnLine", 0.06f);
        Invoke("Execute", Delay);  
    }

    public void Execute()
    {
        for (int i = 0; i < PosTemp.Count; i++)
        {
            RaycastHit2D[] hitInfo = Physics2D.BoxCastAll(OreginTemp[i], new Vector2(Size * PlayerStats.sharedInstance.areaMultiplier / 100f, Size * PlayerStats.sharedInstance.areaMultiplier / 100f), AngleTemp[i], PosTemp[i].normalized);
            //RaycastHit2D[] hitInfo = Physics2D.BoxCastAll(transform.position, new Vector2(Size * PlayerStats.sharedInstance.areaMultiplier / 100f, Size * PlayerStats.sharedInstance.areaMultiplier / 100f), AngleTemp[i], ShotGun.CubeList[i].transform.up);

            for (int j = 0; j < hitInfo.Length; j++)
            {
                if (hitInfo[j].transform.GetComponent<Health>() != null && IgnoreObj.Contains(hitInfo[j].transform.gameObject) == false)
                {
                    Instantiate(ImpactObj, hitInfo[j].transform.position, Quaternion.Euler(0, 0, 0));
                    IgnoreObj.Add(hitInfo[j].transform.gameObject);
                }
            }
        }
        Destroy(gameObject);
    }

    void SpawnLine()
    {
        //Debug.Log("DoDo");
        for (int i = 0; i < OreginTemp.Count; i++)
        {
            GameObject LineObjTemp;
            LineObjTemp = Instantiate(ShotGun.LineObj, OreginTemp[i], Quaternion.Euler(0, 0, AngleTemp[i]));
            KnedlikLib.ScaleParticleByFloat(LineObjTemp, 1, true);
            Rigidbody2D RbTemp = LineObjTemp.GetComponent<Rigidbody2D>();
            RbTemp.AddForce(LineObjTemp.transform.up * ShotGun.LineSpeed, ForceMode2D.Impulse);
        }
    }


    
}
