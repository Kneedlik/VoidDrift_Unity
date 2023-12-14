
using System.Diagnostics;
using UnityEngine;

public class dropXP : MonoBehaviour
{
    public int xpValue;

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
        xpValue = (int)pom;
        levelingSystem.instance.addXp(xpValue);
       // UnityEngine.Debug.Log(xpValue);
    }

}
