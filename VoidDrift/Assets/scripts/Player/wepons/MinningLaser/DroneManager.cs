using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    [SerializeField] List<LaserDrone> DroneList = new List<LaserDrone>();
    public float BaseCoolDown;
    [SerializeField] float CoolDown;
    float TimeStamp;
    int CurrentIndex;
    // Start is called before the first frame update

    private void Update()
    {
        if(DroneList.Count == 0)
        {
            return;
        }

        if (TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if (TimeStamp <= 0)
        {
            if (DroneList[CurrentIndex] != null)
            {
                //Debug.Log("Shooting");
                if (DroneList[CurrentIndex].Shoot())
                {
                    TimeStamp = CoolDown;
                    KnedlikLib.IncreaseIndex(ref CurrentIndex, DroneList.Count);
                }
            }else
            {
                KnedlikLib.IncreaseIndex(ref CurrentIndex, DroneList.Count);
            }
        }
    }

    public void ResetDrones(float AsMultiplier)
    {
        Debug.Log("Reseting Drones");
        DroneList.Clear();
        foreach(Transform e in transform)
        {
            if (e != null)
            {
                LaserDrone LaserDroneTemp = e.GetComponent<LaserDrone>();
                if (LaserDroneTemp != null)
                {
                    DroneList.Add(LaserDroneTemp);
                }
            }
        }

        ScaleDamageAll();

        if (DroneList.Count != 0)
        {
            CoolDown = BaseCoolDown / AsMultiplier;
            CoolDown = CoolDown / DroneList.Count;
        }
        CurrentIndex = 0;
    }

    public void DeleteAllDrones()
    {
        foreach (Transform e in transform)
        {
            Destroy(e.gameObject);
        }
        DroneList.Clear();
    }

    public void ScaleDamageAll()
    {
        for (int i = 0; i < DroneList.Count; i++)
        {
            if (DroneList[i] != null)
            {
                DroneList[i].scaleSummonDamage();
            }
        }
    }

    
}
