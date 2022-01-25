using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class CRTDistortion : MonoBehaviour
{
    private Material material;
    private PlayerController player;
    // Start is called before the first frame update

    public float intensity;
    void Start()
    {
        material = new Material(Shader.Find("Hidden/CRTDistortion"));
        Texture2D displacement = Resources.Load<Texture2D>("CRTDistortion");
        if(displacement)
        {
            displacement.wrapMode = TextureWrapMode.Repeat;
            material.SetTexture("_DistortionTex", displacement);
        }
        else
        {
            Debug.Log("unable to find tex: CRTDistortion");
        }
        player = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    void OnRenderImage(RenderTexture source, RenderTexture dest)
    { 
        float dualityStrength = player.health.GetHealth();
        dualityStrength *= 2f;
        dualityStrength = Mathf.Clamp(dualityStrength, 0, 1f);
        material.SetFloat("_strength", 1 - dualityStrength);
        intensity = dualityStrength;
        Graphics.Blit(source, dest, material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
