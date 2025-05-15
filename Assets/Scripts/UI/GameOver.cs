using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameOver : MonoBehaviour
{
    private InputActionAsset _inputActionAsset;
    private InputActionMap _playerInputMap;
    
    [SerializeField] private GameObject gameOverUI;
    
    private GameObject[] _playerInputDeactivate;
    
    public UnityEvent gameOver = new();

    private bool isGameOver;
    private bool stoppedPlayerMovement;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("OrderSystem").GetComponent<OrderSystem>().ordersCompletedList <
            GameObject.FindWithTag("OrderSystem").GetComponent<OrderSystem>().maxOrdersDailyList &&
            GameObject.FindWithTag("Canvas").GetComponent<dayNightCycle>().GetTime == 0)
        {
            gameOver.Invoke();
        }
        else
        {
            isGameOver = false;
            stoppedPlayerMovement = false;
            Time.timeScale = 1;
        }
    }

    public void GameOverEvent()
    {
        isGameOver = true;
        
        Time.timeScale = 0;

        _playerInputMap = _inputActionAsset.FindActionMap("GameOver");
        
        /*
        var allplayers = FindAnyObjectByType<CharacterController>().GetComponent<PlayerMovement>();
        allplayers.GetComponent<PlayerMovement>().enabled = false;
        //var allPlayersMovement = FindObjectsByType<CharacterController>(PlayerMovement).Length;

        _playerInputDeactivate = GameObject.FindGameObjectsWithTag("Player");
        if (!stoppedPlayerMovement)
        {
            for (int p = 0; p < _playerInputDeactivate.Length; p++)
            {
                _playerInputDeactivate[p].transform.parent.GetComponent<PlayerInputManager>().enabled = false;
                stoppedPlayerMovement = true;
            }
        }
        */
    }
}
