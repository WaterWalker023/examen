using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    private InputActionAsset _inputActionAsset;
    private InputActionMap _playerInputMap;
    
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
        var playerInputDeactivate = FindAnyObjectByType<PlayerInputManager>();
        playerInputDeactivate.GetComponent<PlayerInputManager>().enabled = false;
    }

    private void Update()
    {
        if (!hasClicked) return;
        var playerInputActivate = FindAnyObjectByType<PlayerInputManager>();
        playerInputActivate.GetComponent<PlayerInputManager>().enabled = true;
        playerInputActivate.GetComponent<PlayerInputManager>().EnableJoining();
    }

    public void gameBegin()
    {
        hasClicked = true;
        
        startGame.Invoke();
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
