using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    private Rigidbody2D rb;
    private int randomRotation;

    // Start is called before the first frame update
    void Start()
    {
      //  rb = this.GetComponent<Rigidbody2D>();
        randomRotation = Random.Range(-179, 179);
        // rb.rotation = randomRotation;
        transform.rotation = Quaternion.Euler(0, 0, randomRotation);


    }
}
