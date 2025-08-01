using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
    [SerializeField] bool UseConstants = true;
    public float duration = 0.3f;
    SpriteRenderer spriteRenderer;
    Material oreginalMaterial;
    public Material flashMaterial;
    Coroutine flash;

    float coolDown;
    float TrueDuration;

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

        if(UseConstants)
        {
            TrueDuration = Constants.FlashWhiteDuration;
        }else
        {
            TrueDuration = duration;
        }
    }

    public void Flash()
    {
        if(flashMaterial == null)
        {
            return;
        }

        if(coolDown <= 0 && gameObject.activeSelf)
        {
            if (flash != null)
            {
                StopCoroutine(flash);
            }
            flash = StartCoroutine(flashRoutine());     //
        }
    }

    

    IEnumerator flashRoutine()
    {
        spriteRenderer.sharedMaterial = flashMaterial; //
        yield return new WaitForSeconds(TrueDuration);
        spriteRenderer.sharedMaterial = oreginalMaterial;
        coolDown = TrueDuration * 2;
        flash = null;
    }
   
}
