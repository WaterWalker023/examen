using System;
using UnityEngine;

public class AggrowState : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        var test = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
        foreach (var player in test)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
