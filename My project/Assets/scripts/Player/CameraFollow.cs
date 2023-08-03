
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    public Transform target;
    public Vector3 offset;
    public float smoothSpeed;
    public float smoothOffset ;
    private Vector2 mousepos;

    public Camera cam;
    public float Ymin = 20;
    public float Xmin = 35;
    public float MaxDistance;
    private Vector2 distance;

    //screenShake
    [SerializeField] float shakePower;
    float realPower;
    float shakeFadeTime;
    [SerializeField] float shakeTime;
    [SerializeField] float shakeRotation;
    float realRotation;
    float timeStamp;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
       
    }

 

    private void FixedUpdate()
    {
        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousepos.x = mousepos.x - target.position.x;
        mousepos.x = mousepos.x - offset.x;

        mousepos.y = mousepos.y - target.position.y;
        mousepos.y = mousepos.y - offset.y;



        if (mousepos.x > Xmin)
        {
            distance.x = mousepos.x - Xmin;
        }

        if (mousepos.x < Xmin * -1)
        {
            distance.x = mousepos.x + Xmin;
        }

        if (distance.x > 5)
        {
            distance.x = 5;
        }

        if (distance.x < -5)
        {
            distance.x = -5;
        }

        if (mousepos.y > Ymin)
        {
            distance.y = mousepos.y - Ymin;
        }

        if (mousepos.y < Ymin * -1)
        {
            distance.y = mousepos.y + Ymin;
        }

        if (distance.y > MaxDistance)
        {
            distance.y = 5;
        }

        if (distance.y < MaxDistance * -1)
        {
            distance.y = MaxDistance * -1;
        }

        if (mousepos.x < Xmin && mousepos.x > Xmin * -1)
        {
            distance.x = 0;
        }

        if (mousepos.y < Ymin && mousepos.y > Ymin * -1)
        {
            distance.y = 0;
        }

        // offset.x = distance.x;
        // offset.y = distance.y;

        offset.x += (distance.x - offset.x) * smoothOffset;
        offset.y += (distance.y - offset.y) * smoothOffset;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        if(timeStamp > 0)
        {
            timeStamp -= Time.deltaTime;

            float xPower = Random.Range(-1f, 1f) * realPower;
            float yPower = Random.Range(-1f, 1f) * realPower;

            transform.position += new Vector3(xPower, yPower,0);
            realPower = Mathf.MoveTowards(realPower,0f, shakeFadeTime * Time.deltaTime);
            realRotation = Mathf.MoveTowards(realRotation,0f, shakeFadeTime * shakeRotation * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0, 0, realRotation);
    }

    

    public void startShake(float powerMultiplier,float timeMultiplier)
    {
        timeStamp = shakeTime * timeMultiplier;
        realPower = shakePower * powerMultiplier;

        shakeFadeTime = shakePower / shakeTime;
        realRotation = realPower * shakeRotation;
    }


}
