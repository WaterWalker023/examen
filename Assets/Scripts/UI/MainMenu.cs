using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    private InputActionAsset _inputActionAsset;
    private InputActionMap _playerInputMap;

    private PlayerInputManager playerInputActivate;
    
    private bool hasClicked;

    public bool HasClicked
    {
        get
        {
            return hasClicked;
        }
    }

    public UnityEvent startGame = new();

    private void Start()
    {
        playerInputActivate = FindAnyObjectByType<PlayerInputManager>();
        playerInputActivate.GetComponent<PlayerInputManager>().enabled = false;
    }

    public void gameBegin()
    {
        hasClicked = true;
        
        startGame.Invoke();
        
        playerInputActivate.GetComponent<PlayerInputManager>().enabled = true;
        playerInputActivate.GetComponent<PlayerInputManager>().EnableJoining();
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
