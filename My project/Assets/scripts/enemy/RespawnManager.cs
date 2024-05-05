using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;
    public float distance;
    [SerializeField] private Transform cam;
    
    private void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame

    public void respawn(float time,GameObject prefab,Vector3 location, bool destroy)
    {
        if (prefab != null)
        {
            StartCoroutine(respawnRutine(time, prefab, location, destroy));
        }
    }

    

    IEnumerator respawnRutine(float time,GameObject prefab,Vector3 location,bool destroy)
    {
        Transform pom = transform.parent;

        if (prefab == null)
        {
            yield return null;
        }

       if(destroy == false)
        {
            prefab.transform.parent = gameObject.transform;
            prefab.SetActive(false);
        }

        yield return new WaitForSeconds(time);

        while(Vector3.Distance(cam.position,location) < distance)
        {
            yield return new WaitForSeconds(1);
        }

        if(destroy == false)
        {
            prefab.transform.parent = pom;
            prefab.SetActive(true);
            prefab.transform.position = location;
            Health health = prefab.GetComponent<Health>();
            health.setUp();
        }else
        {
            Instantiate(prefab,location,Quaternion.Euler(0,0,0));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(cam.position,distance);
    }
}
