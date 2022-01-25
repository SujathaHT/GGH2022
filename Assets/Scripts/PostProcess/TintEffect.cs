using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class TintEffect : MonoBehaviour
{
    private Material material;

    private PlayerController player; 

    public float intensity;

    void Awake()
    {
        material = new Material(Shader.Find("Hidden/TintEffect"));
        player = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        // if(intensity == 0)
        // {
        //     Graphics.Blit(source, dest);
        //     return;
        // }

        float dualityStrength = player.health.GetHealth();
        dualityStrength = Mathf.Clamp(dualityStrength, 0, 1f);
        material.SetFloat("_tintBlend", 1 - dualityStrength);
        Graphics.Blit(source, dest, material);
        intensity = 1 - dualityStrength;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
