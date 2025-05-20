using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseGameUI;

    private bool isOpen;
    
    private PlayerInputManager _playerInputDeactivate;

    public UnityEvent resumeGame = new();

    public UnityEvent pauseGame = new();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseGameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        if (!GameObject.FindWithTag("Canvas").GetComponent<MainMenu>().HasClicked) return;
        pauseGame.Invoke();

        Time.timeScale = 0;
        
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find("Canvas").GetComponent<GameOver>().enabled = false;

        if (isOpen)
        {
            GameResumed();
        }
        else
        {

            isOpen = true;
        }
        var allplayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in allplayers)
        {
            player.transform.parent.GetComponent<PlayerMovement>().enabled = !isOpen;
            player.transform.parent.parent.GetComponentInChildren<CinemachineOrbitalFollow>().enabled = !isOpen;
        }
    }

    public void GameResumed()
    {
        Time.timeScale = 1;
        
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        
        var allplayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in allplayers)
        {
            player.transform.parent.GetComponent<PlayerMovement>().enabled = true;
            player.transform.parent.parent.GetComponentInChildren<CinemachineOrbitalFollow>().enabled = true;
        }
        
        isOpen = false;
        
        resumeGame.Invoke();
        
        GameObject.Find("Canvas").GetComponent<GameOver>().enabled = true;
    }

    public void BackToMenu(string sceneName)
    {
        Time.timeScale = 1;

        isOpen = false;

        SceneManager.LoadScene(sceneName);
    }
}
