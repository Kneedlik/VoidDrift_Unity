using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidDrop : MonoBehaviour
{
    public GameObject Item1;
    public GameObject Item2;
    private GameObject Item;
    private bool isQuit;

     

    private void OnDestroy()
    {
        if (!isQuit)
        {
            int randomNumber = Random.Range(0, 100);
            Debug.Log(randomNumber);

            Vector3 pos = transform.position;
            float offsetX = Random.Range(-30, 30);
            float offsetY = Random.Range(-30, 30);

            offsetX = offsetX / 10;
            offsetY = offsetY / 10;

            pos.x = pos.x + offsetX;
            pos.y = pos.y + offsetY;

            if (randomNumber <= 50)
            {
                Item = decideObject();
                Instantiate(Item, pos, Quaternion.identity);
            }
            else if (randomNumber <= 80)
            {
                Item = decideObject();
                Instantiate(Item, pos, Quaternion.identity);

                Item = decideObject();
                Instantiate(Item, pos, Quaternion.identity);
            }
            else
            {
                Item = decideObject();
                Instantiate(Item, pos, Quaternion.identity);

                Item = decideObject();
                Instantiate(Item, pos, Quaternion.identity);

                Item = decideObject();
                Instantiate(Item, pos, Quaternion.identity);
            }
        }
    }

    GameObject decideObject()
    {
        int randomNumber2 = Random.Range(0, 100);

        if (randomNumber2 <= 50)
        {
            return Item1;
        }
        else
        {
            return Item2;
        }
    }
}
