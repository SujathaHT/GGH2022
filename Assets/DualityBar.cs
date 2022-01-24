using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platformer.Mechanics;

public class DualityBar : MonoBehaviour
{
    public Image healthBarImage;
    private PlayerController player;

    public void Awake()
    {

        GameObject playerObject = GameObject.Find("Player");
        if (playerObject)
        {
            player = playerObject.GetComponent<PlayerController>();
            if (!player)
            {
                Debug.Log("PlayerController not found" + this.GetType().ToString());
            }
            else
            {
                healthBarImage.fillAmount = player.health ? player.health.GetHealth() : 0.2f;
            }
        }
        else
        {
            Debug.Log("player not found" + this.GetType().ToString());
        }
    }
    public void UpdateHealthBar()
    {
        if(player)
        {
            // Simply use the health value directly
            healthBarImage.fillAmount = player.health.GetHealth();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        
    }
}
