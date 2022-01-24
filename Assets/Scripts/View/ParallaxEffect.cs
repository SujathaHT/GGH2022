using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startPosition;
    public float parallaxEffectFactor;
    private GameObject cam ;
    // public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        if(!cam)
        {
            Debug.Log("camera not found");
        }
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate(){
        float t = cam.transform.position.x * (1 - parallaxEffectFactor);
        float dist = cam.transform.position.x * parallaxEffectFactor;
        transform.position = new Vector3(startPosition + dist, transform.position.y, transform.position.z);

        if (t > startPosition + length)
        {
            startPosition += length;
        }
        else if (t < startPosition - length)
        {
            startPosition -= length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
