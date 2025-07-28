using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearDamageAura : MonoBehaviour
{
    [SerializeField] LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.OnLevel += UpdateSize;
        SetUpLine();
        UpdateSize();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        NearDamageSystem.instance.AddToList(collision.gameObject);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        NearDamageSystem.instance.RemoveFromList(collision.gameObject);
    }

    public void UpdateSize()
    {
        float size = 1f * (PlayerStats.sharedInstance.areaMultiplier / 100f);
        size = size * MasterManager.Instance.PlayerInformation.SizeMultiplier;
        transform.localScale = new Vector3(size, size, 1f);
    }

    public void SetUpLine()
    {
        CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
        float radius = circle.radius;
        KnedlikLib.DrawCircle(line, radius, 250);
    }
}
