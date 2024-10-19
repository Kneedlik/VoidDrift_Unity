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
            //Debug.Log("Shooting");
            if (DroneList[CurrentIndex].Shoot())
            {
                TimeStamp = CoolDown;
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
            LaserDrone LaserDroneTemp = e.GetComponent<LaserDrone>();
            if (LaserDroneTemp != null)
            {
                LaserDroneTemp.scaleSummonDamage();
                DroneList.Add(LaserDroneTemp);
            }
        }

        if (DroneList.Count != 0)
        {
            CoolDown = BaseCoolDown / AsMultiplier;
            CoolDown = CoolDown / DroneList.Count;
        }
    }

    
}
