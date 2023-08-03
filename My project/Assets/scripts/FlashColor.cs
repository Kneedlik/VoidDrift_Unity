using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
    public float duration = 0.3f;
    SpriteRenderer spriteRenderer;
    Material oreginalMaterial;
    public Material flashMaterial;
    Coroutine flash;

    float coolDown;

    private void Update()
    {
        if(coolDown >= 0)
        {
            coolDown -= Time.deltaTime;
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        oreginalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        if(coolDown <= 0 && gameObject.activeSelf)
        {
            if (flash != null)
            {
                StopCoroutine(flash);
            }
            flash = StartCoroutine(flashRoutine());
        }
    }

    

    IEnumerator flashRoutine()
    {
        
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = oreginalMaterial;
        coolDown = duration * 2;
        flash = null;
    }
   
}
