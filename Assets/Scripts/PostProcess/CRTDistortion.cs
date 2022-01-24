using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRTDistortion : MonoBehaviour
{
    private Material material;

    public float distortionStrength;
    // Start is called before the first frame update
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
    }

    void OnRenderImage(RenderTexture source, RenderTexture dest)
    { 
        material.SetFloat("_strength", distortionStrength);
        Graphics.Blit(source, dest, material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
