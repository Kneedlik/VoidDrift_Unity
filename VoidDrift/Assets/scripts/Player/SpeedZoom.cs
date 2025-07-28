using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZoom : MonoBehaviour
{
    public Camera camera;
    public PlayerMovement playerMovement;

    public float maxZoom;
    public float minZoom;
    public float plusSpeed;
    public float minusSpeed;
    public float zoomDelay;
    float speed;
    public float speedBreakPoint;

    float timeStamp;

    void Start()
    {
        minZoom = camera.orthographicSize;
    }

    void Update()
    {
        speed = playerMovement.speed;

        if(speed >= speedBreakPoint)
        {
            if(timeStamp <= zoomDelay)
            {
                timeStamp += Time.deltaTime;

            }
           
        }else if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;
        }
         

    }

    private void FixedUpdate()
    {

        if (timeStamp >= zoomDelay)
        {
            if (camera.orthographicSize < maxZoom)
            {
                camera.orthographicSize += plusSpeed;
            }

        }
        else if (timeStamp <= 0)
        {
            if (camera.orthographicSize > minZoom)
            {
                camera.orthographicSize -= minusSpeed;
            }
        }
    }
}
