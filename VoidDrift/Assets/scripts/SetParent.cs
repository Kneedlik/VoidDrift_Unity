using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    Transform minimap;
    GameObject FirstParent;

    void Start()
    {
        followPosition follow = GetComponent<followPosition>();
        if (follow != null)
        {
            follow.obj = transform.parent.gameObject;
        }
        FirstParent = transform.parent.gameObject;

        minimap = GameObject.FindWithTag("MapIcons").GetComponent<Transform>();
        transform.parent = minimap;
    }

    private void Update()
    {
        if (FirstParent == null)
        {
            Destroy(gameObject);
        }
    }



}
