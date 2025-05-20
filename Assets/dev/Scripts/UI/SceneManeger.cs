using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour
{
    private bool _hasViewedIntro;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void NextScene()
    {
        PlayerPrefs.SetString("hasViewedIntro","True");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
