using UnityEngine;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    private bool hasClicked = false;

    public bool HasClicked
    {
        get
        {
            return hasClicked;
        }
    }

    public UnityEvent startGame = new();

    // Update is called once per frame
    void Update()
    {
        if (!hasClicked) { return; }
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
