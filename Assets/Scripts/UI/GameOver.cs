using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    
    private PlayerInputManager _playerInputDeactivate;
    
    public UnityEvent gameOver = new();

    private bool isGameOver;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("OrderSystem").GetComponent<OrderSystem>().ordersCompletedList <
            GameObject.FindWithTag("OrderSystem").GetComponent<OrderSystem>().maxOrdersDailyList &&
            GameObject.FindWithTag("Canvas").GetComponent<dayNightCycle>().GetTime == 0)
        {
            GameOverEvent();
        }
        else
        {
            isGameOver = false;
            //Time.timeScale = 1;
        }
    }

    private void GameOverEvent()
    {
        isGameOver = true;
        
        Time.timeScale = 0;
        
        _playerInputDeactivate = FindAnyObjectByType<PlayerInputManager>();
        _playerInputDeactivate.GetComponent<PlayerInputManager>().enabled = false;
        
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        
        gameOver.Invoke();
    }
}
