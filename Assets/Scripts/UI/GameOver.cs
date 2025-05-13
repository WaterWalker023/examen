using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    
    private PlayerInputManager _playerInputDeactivate;
    
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
        
        _playerInputDeactivate = FindAnyObjectByType<PlayerInputManager>();
        _playerInputDeactivate.GetComponent<PlayerInputManager>().enabled = false;

        var allplayers = CharacterController.FindAnyObjectByType<CharacterController>();

        if (!stoppedPlayerMovement)
        {
            allplayers.GetComponent<PlayerMovement>().enabled = false;
            stoppedPlayerMovement = true;
        }
    }
}
