using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startPosition;
    public float parallaxEffectFactor;
    private GameObject cam ;
    // public GameObject camera;

    private bool bInstantiatedNewPrefab = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        if(!cam)
        {
            Debug.Log("camera not found");
        }
        startPosition = transform.position.x;
        
        length = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    void FixedUpdate(){
        float t = cam.transform.position.x * (1 - parallaxEffectFactor);
        float dist = cam.transform.position.x * parallaxEffectFactor;
        transform.position = new Vector3(startPosition + cam.transform.position.x - dist, transform.position.y, transform.position.z);
        // Debug.Log(transform.position);
        // Debug.Log(cam.transform.position.x + " -> " + (startPosition + length).ToString());
        // TODO: delete far off background prefabs that got created, for performance
        if(!bInstantiatedNewPrefab)
        {
            float camPositionRight = cam.transform.position.x + length;
            //float camPositionRight = t + length;
            float boundRight  = startPosition + length;

            if (camPositionRight > boundRight)
                    {
                        string prefabName = gameObject.name.Replace("(Clone)", "");
                        // Debug.Log(prefabName + " on the right ");
                        Object obj = Resources.Load(prefabName);
                        Instantiate(obj, 
                            new Vector3(startPosition + GetComponent<SpriteRenderer>().bounds.size.x, 
                                transform.position.y, transform.position.z), Quaternion.identity);
                        bInstantiatedNewPrefab = true;
                    }
                    else if (cam.transform.position.x - length < startPosition - length)
                    {
                        // TODO: fix
                    //     string prefabName = gameObject.name.Replace("(Clone)", "");
                    //     Debug.Log(prefabName + " on the left "); 
                    //     Object obj = Resources.Load(prefabName);
                    //     Instantiate(obj, 
                    //         new Vector3(startPosition - GetComponent<SpriteRenderer>().bounds.size.x, 
                    //             transform.position.y, transform.position.z), Quaternion.identity);
                    //     bInstantiatedNewPrefab = true;
                    // 
                    }

                }
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
