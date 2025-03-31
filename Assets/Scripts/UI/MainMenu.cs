using UnityEngine;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    private bool hasClicked;

    public bool HasClicked
    {
        get
        {
            return hasClicked;
        }
    }

    public UnityEvent startGame = new();

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
