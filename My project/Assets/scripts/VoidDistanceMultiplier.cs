using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDistanceMultiplier : MonoBehaviour
{
    static public VoidDistanceMultiplier Instance;

    public float DistanceX;
    public float DistanceY;
    [SerializeField] float Distance;
    public bool OutOffBounds = false;
    Transform Player;

    public float HealthMultiplier = 1;
    public float XPMultiplier = 1;
    public float SpeedMultiplier = 1;
    public float MaxSpeedMultiplier = 2.5f;
    public float SpawnRateMultiplier = 1;
    public float MaxSpawnRateMultiplier = 2;

    [SerializeField] float HealthScaling;
    [SerializeField] float XPScaling;
    [SerializeField] float SpeedScaling;
    [SerializeField] float SpawnRateScaling;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float posX = Mathf.Abs(Player.position.x) - transform.position.x;
        float posY = Mathf.Abs(Player.position.y) - transform.position.y;
        posX -= DistanceX;
        posY -= DistanceY;

        if (posX > posY)
        {
            Distance = posX;
        }else Distance = posY;

        if(Distance > 0)
        {
            OutOffBounds = true;
        }else
        {
            HealthMultiplier = 1;
            XPMultiplier = 1;
            SpeedMultiplier = 1;
            OutOffBounds = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,new Vector3(DistanceX * 2,DistanceY * 2,0 ));
    }
}
