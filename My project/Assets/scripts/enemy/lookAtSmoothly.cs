using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtSmoothly : MonoBehaviour
{
    Transform target;
    public float rotationSpeed;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        Quaternion rotTarget3D = Quaternion.LookRotation(target.position - new Vector3(transform.position.x, transform.position.y));
        Quaternion rotTarget = Quaternion.Euler(0, 0, rotTarget3D.eulerAngles.y < 180 ? 270 - rotTarget3D.eulerAngles.x : rotTarget3D.eulerAngles.x - 270);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotationSpeed * Time.deltaTime);
    }
}
