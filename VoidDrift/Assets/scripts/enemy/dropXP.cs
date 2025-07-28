
using System.Diagnostics;
using UnityEngine;

public class dropXP : MonoBehaviour
{
    public bool UsePrefab = false; 
    public int xpValue;
    public float percentValue = 0;
    [SerializeField] GameObject XpPrefab;
    [SerializeField] Transform Point;

    /*   private void OnDestroy()
       {
           if (!isQuit)
           {
               float pom = xpValue * (PlayerStats.sharedInstance.EXPmultiplier / 100);
               xpValue = (int)pom;
               levelingSystem.instance.addXp(xpValue);

           }
       }
    */

   public void addXP()
    {
        float pom = xpValue * (PlayerStats.sharedInstance.EXPmultiplier / 100);
        pom += percentValue * levelingSystem.instance.xpNeeded;

        if (UsePrefab == false)
        {
            xpValue = (int)pom;
            levelingSystem.instance.addXp(xpValue);
        }else
        {
            if (XpPrefab != null)
            {
                GameObject Obj;
                if (Point != null)
                {
                    Obj = Instantiate(XpPrefab, Point.position, Quaternion.Euler(0, 0, 0));
                }
                else
                {
                    Obj = Instantiate(XpPrefab, transform.position, Quaternion.Euler(0, 0, 0));
                }
                Obj.GetComponent<addXpOnCollison>().xpValue = xpValue;
                Obj.GetComponent<addXpOnCollison>().PercentValue = percentValue;

            }
        }

        
       // UnityEngine.Debug.Log(xpValue);
    }

}
