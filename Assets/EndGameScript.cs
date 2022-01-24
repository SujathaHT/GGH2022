using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class EndGameScript : MonoBehaviour
{
    public GameObject WonText;
    public GameObject LostText;
    void Awake()
    {
        if(PlayerController.playerWon)
        {
            LostText.SetActive(false);
            WonText.SetActive(true);
        }
        else
        {
            WonText.SetActive(false);
            LostText.SetActive(true);
        }
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
