using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;
using System.Drawing;

public class Hovl_Laser : MonoBehaviour
{
    //public int damageOverTime = 30;

    public GameObject HitEffect;
    public float HitOffset = 0;
    public bool useLaserRotation = false;

    public float MaxLength;
    [HideInInspector] public LineRenderer Laser;

    public float MainTextureLength = 1f;
    public float NoiseTextureLength = 1f;
    private Vector4 Length = new Vector4(1,1,1,1);
    
    //One activation per shoot
    private bool LaserSaver = false;
    private bool UpdateSaver = false;

    private ParticleSystem[] Effects;
    private ParticleSystem[] Hit;

    Vector3 StartPos;
    Vector3 EndPos;
    bool Endless;
    bool Enabled = false;

    void Awake()
    {
        //Get LineRender and ParticleSystem components from current prefab;  
        Laser = GetComponent<LineRenderer>();
        Effects = GetComponentsInChildren<ParticleSystem>();
        Hit = HitEffect.GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        Laser.material.SetTextureScale("_MainTex", new Vector2(Length[0], Length[1]));                    
        Laser.material.SetTextureScale("_Noise", new Vector2(Length[2], Length[3]));
        //To set LineRender position
        if (Laser != null && UpdateSaver == false)
        {
            if (Enabled)
            {
                Laser.SetPosition(0, StartPos);
                //DELETE THIS IF YOU WANT USE LASERS IN 2D
                                //ADD THIS IF YOU WANNT TO USE LASERS IN 2D: RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, MaxLength);       
                if (Endless == false)//CHANGE THIS IF YOU WANT TO USE LASERRS IN 2D: if (hit.collider != null)
                {
                    //End laser position if collides with object
                    Laser.SetPosition(1, EndPos);

                    //HitEffect.transform.position = hit.point + hit.normal * HitOffset;
                    HitEffect.transform.position = EndPos;
                    if (useLaserRotation)
                        HitEffect.transform.rotation = transform.rotation;
                    else
                        HitEffect.transform.LookAt(EndPos);
                    //HitEffect.transform.LookAt(hit.point + hit.normal);

                    foreach (var AllPs in Effects)
                    {
                        if (!AllPs.isPlaying) AllPs.Play();
                    }
                    //Texture tiling
                    Length[0] = MainTextureLength * (Vector3.Distance(StartPos, EndPos));
                    Length[2] = NoiseTextureLength * (Vector3.Distance(StartPos, EndPos));

                }
                else
                {
                    //End laser position if doesn't collide with object
                    //var EndPos = transform.position + transform.forward * MaxLength;
                    Laser.SetPosition(1, EndPos);
                    HitEffect.transform.position = EndPos;
                    foreach (var AllPs in Hit)
                    {
                        if (AllPs.isPlaying) AllPs.Stop();
                    }
                    //Texture tiling
                    Length[0] = MainTextureLength * (Vector3.Distance(StartPos, EndPos));
                    Length[2] = NoiseTextureLength * (Vector3.Distance(StartPos, EndPos));
                    //LaserSpeed[0] = (LaserStartSpeed[0] * 4) / (Vector3.Distance(transform.position, EndPos)); {DISABLED AFTER UPDATE}
                    //LaserSpeed[2] = (LaserStartSpeed[2] * 4) / (Vector3.Distance(transform.position, EndPos)); {DISABLED AFTER UPDATE}
                }
            }
            //Insurance against the appearance of a laser in the center of coordinates!
            if (Laser.enabled == false && LaserSaver == false)
            {
                LaserSaver = true;
                Laser.enabled = true;
            }
        }  
    }

    public void SetUp(Vector3 StartPosition,Vector3 EndPosition,bool EndlessTemp)
    {
        if (StartPosition != Vector3.zero)
        {
            StartPos = StartPosition;
        }
        if (EndPosition != Vector3.zero)
        {
            EndPos = EndPosition;
        }
        Endless = EndlessTemp;
    }

    public void SetSize(float Width,float HitArea)
    {
        HitEffect.transform.localScale = new Vector3(HitArea,HitArea,HitArea);
        Laser.widthMultiplier = Width;
    }

    public void EnablePrepare()
    {
        Enabled = true;
        Laser.enabled = true;
        UpdateSaver = false;
    }

    public void DisablePrepare()
    {
        Enabled = false;
        Laser.enabled = false;

        if (Laser != null)
        {
            Laser.enabled = false;
        }
        UpdateSaver = true;
        //Effects can = null in multiply shooting
        if (Effects != null)
        {
            foreach (var AllPs in Effects)
            {
                if (AllPs.isPlaying) AllPs.Stop();
            }
        }
    }
}
