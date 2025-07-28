using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterAI : MonoBehaviour
{
    [SerializeField] List<GameObject> Jets = new List<GameObject>();
    [SerializeField] List<Transform> Points = new List<Transform>();
    [SerializeField] float JetGridDistance;
    Transform Player;
    float Distance;
    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(Player.position, transform.position);

        if (Distance < JetGridDistance) 
        {
            for (int i = 0; i < Jets.Count; i++)
            {
                if (Jets[i] != null)
                {
                    if (Jets[i].activeInHierarchy == false)
                    {
                        imperial_jet_ai AI = Jets[i].GetComponent<imperial_jet_ai>();
                        AI.SetAlert(false);
                        AI.SetPatrol(true);

                        Jets[i].SetActive(true);
                        Jets[i].transform.position = Points[i].transform.position;
                    }
                }
            }
        }else
        {
            for (int i = 0; i < Jets.Count; i++)
            {
                if (Jets[i] != null)
                {
                    if (Jets[i].activeInHierarchy)
                    {
                        Jets[i].SetActive(false);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, JetGridDistance);
    }
}
