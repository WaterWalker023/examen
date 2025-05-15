using System;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerState : MonoBehaviour
{
    new List<Vector3> TheWay = new List<Vector3>();
    private Vector3 currentmovement;
    // Update is called once per frame
    void Update()
    {
        
        var Players = FindObjectsByType<CharacterController>(FindObjectsSortMode.None);

        if (!Players[0].GetComponent<PlayerMovement>().parseInput)
        {
            return;
        }
        currentmovement = currentdection();
        
        
        
        if (currentmovement == Vector3.zero)
        {
            return;
        }
        
        Debug.Log(currentmovement);
        for (int player = 0; player < Players.Length; player++)
        {
            Players[player].GetComponent<CharacterController>()
                .Move(currentmovement * Time.deltaTime);
        }
        
    }

    public void waytogo(Vector3 way)
    {
        TheWay.Add(way);
    }

    Vector2 currentdection()
    {
        var derection = Vector3.zero;
        for (int i = 0; i < TheWay.Count; i++)
        {
            derection += TheWay[i];
        }
        derection = derection / TheWay.Count;
        Debug.Log(TheWay.Count);
        TheWay.Clear();
        return derection;
    }
    
}
