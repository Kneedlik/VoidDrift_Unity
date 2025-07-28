using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtSmoothly : MonoBehaviour
{
    Transform target;
    public float rotationSpeed;
    [SerializeField] bool Flip180 = false;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        Quaternion rotTarget3D = Quaternion.LookRotation(target.position - new Vector3(transform.position.x, transform.position.y));
        Quaternion rotTarget = Quaternion.Euler(0, 0, rotTarget3D.eulerAngles.y < 180 ? 270 - rotTarget3D.eulerAngles.x : rotTarget3D.eulerAngles.x - 270);

       // Quaternion.Inverse(rotTarget);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotationSpeed * Time.deltaTime);

      //  if (Flip180)
      //  {
      //      transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z - 180);
       // }  
    }
}
