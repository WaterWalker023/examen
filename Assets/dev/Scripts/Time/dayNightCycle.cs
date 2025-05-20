using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class dayNightCycle : MonoBehaviour
{
    [SerializeField] private float timeGame;
    [SerializeField] private int displayTime;

    [SerializeField] private float totalDegreesToTravel;

    [SerializeField] private float startingOffset;

    [SerializeField] private TMP_Text timeGameTXT;

    [SerializeField] private GameObject directionalLight;

    [SerializeField] private float totaltime;

    [SerializeField] private float totalDisplayTime;

    public float GetTime
    {
        get
        {
            return timeGame;
        }
        set
        {
            timeGame = value;
        }
    }
    
    private void Start()
    {
        totaltime = timeGame;
        totalDisplayTime = displayTime;
        timeGame = Convert.ToInt32(displayTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindWithTag("Canvas").GetComponent<MainMenu>().HasClicked) return;
        
        if (timeGame == 0) return;

        timeGame = timeGame - Time.deltaTime;

        displayTime = (int) (timeGame / totaltime * totalDisplayTime);

        timeGameTXT.text = Mathf.Floor(displayTime / 60).ToString("00") + ":" + Mathf.Floor(displayTime % 60).ToString("00");

        //directionalLight.transform.rotation = Quaternion.Euler((timeGame / totaltime * totalDegreesToTravel) + startingOffset, 0, 0);

        if (!(timeGame <= 0)) return;
        timeGame = 0;
    }
}
