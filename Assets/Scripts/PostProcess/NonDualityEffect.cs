using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using Platformer.Mechanics;

public class NonDualityEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private Material material;

    private PlayerController player;

    public float intensity;

    void Awake()
    {
        material = new Material(Shader.Find("Hidden/NonDualityShader"));
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        // The health of player depics duality strength
        float dualityStrength = player.health.GetHealth();
        dualityStrength = Mathf.Clamp(dualityStrength, 0, 1f);
        material.SetFloat("_strength", 1f - dualityStrength);
        intensity = dualityStrength;
        // suja
        Graphics.Blit(source, dest, material);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
