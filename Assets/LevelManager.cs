using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using Platformer.Mechanics;
using Platformer.Core;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float respawnDelay;
    private PlayerController player;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void EndGame()
    {
        PlayerController.playerWon=true;
        SceneManager.LoadScene("EndScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    // public void Respawn()
    // {
    //     StartCoroutine(SpawnCoroutine());
    // }

    // IEnumerator SpawnCoroutine()
    // {
    //         yield return new WaitForSeconds(respawnDelay);
    //         Simulation.Schedule<PlayerSpawn>(2);
    // }

}
