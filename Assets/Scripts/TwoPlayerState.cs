using System;
using UnityEngine;

public class TwoPlayerState : MonoBehaviour
{
    private void OnEnable()
    {
        var Players = FindObjectsByType<CharacterController>(FindObjectsSortMode.None);
        for (int player = 0; player < Players.Length; player++)
        {
            Players[player].GetComponent<PlayerMovement>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
