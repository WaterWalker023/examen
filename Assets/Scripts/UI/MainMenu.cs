using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    private InputActionAsset _inputActionAsset;
    private InputActionMap _playerInputMap;

    private PlayerInputManager _playerInputActivate;
    
    private bool _hasClicked;

    public bool HasClicked
    {
        get
        {
            return _hasClicked;
        }
    }

    public UnityEvent startGame = new();

    private void Start()
    {
        _playerInputActivate = FindAnyObjectByType<PlayerInputManager>();
        _playerInputActivate.GetComponent<PlayerInputManager>().enabled = false;
    }

    public void gameBegin()
    {
        _hasClicked = true;
        
        startGame.Invoke();
        
        _playerInputActivate.GetComponent<PlayerInputManager>().enabled = true;
        _playerInputActivate.GetComponent<PlayerInputManager>().EnableJoining();
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
