using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    [SerializeField] List<LaserDrone> DroneList = new List<LaserDrone>();
    public float BaseCoolDown;
    float CoolDown;
    MiningLaser Laser;
    float TimeStamp;
    int CurrentIndex;
    // Start is called before the first frame update
    void Start()
    {
        Laser = GameObject.FindWithTag("Weapeon").GetComponent<MiningLaser>();
    }

    private void Update()
    {
        if (TimeStamp > 0)
        {
            TimeStamp -= Time.deltaTime;
        }

        if (TimeStamp <= 0)
        {
            if (DroneList[CurrentIndex].Shoot())
            {
                TimeStamp = CoolDown;
                KnedlikLib.IncreaseIndex(ref CurrentIndex, DroneList.Count);
            }
        }
    }

    public void ResetDrones()
    {
        DroneList.Clear();
        foreach(Transform e in transform)
        {
            LaserDrone LaserDroneTemp = e.GetComponent<LaserDrone>();
            if (LaserDroneTemp != null)
            {
                LaserDroneTemp.scaleSummonDamage();
                DroneList.Add(LaserDroneTemp);
            }
        }
        CoolDown = BaseCoolDown / Laser.ASmultiplier;
        CoolDown = CoolDown / DroneList.Count;
    }

    
}
