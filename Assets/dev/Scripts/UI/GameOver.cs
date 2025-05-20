using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    
    public UnityEvent gameOver = new();

    private bool isGameOver;
    
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("OrderSystem").GetComponent<OrderSystem>().ordersCompletedList <
            GameObject.FindWithTag("OrderSystem").GetComponent<OrderSystem>().maxOrdersDailyList &&
            GameObject.FindWithTag("Canvas").GetComponent<dayNightCycle>().GetTime == 0)
        {
            gameOver.Invoke();
            var allplayers = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in allplayers)
            {
                player.transform.parent.GetComponent<PlayerMovement>().enabled = false;
                player.transform.parent.parent.GetComponentInChildren<CinemachineOrbitalFollow>().enabled = false;
            }
        }
        else
        {
            isGameOver = false;
            Time.timeScale = 1;
        }
    }

    public void GameOverEvent()
    {
        isGameOver = true;
        
        Time.timeScale = 0;
    }
}
