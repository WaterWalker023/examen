using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Triggerbox : MonoBehaviour
{
    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerStay;
    public UnityEvent OnPlayerExit;
    
    private List<GameObject>  _players = new List<GameObject>();
    
    public List<GameObject> Players { get { return _players; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerInput>())
        {
            _players.Add(other.gameObject);
            OnPlayerEnter.Invoke();
        }
    }


    private void Update()
    {
        if (_players.Count == 0) return;
        foreach (var player in _players)
            OnPlayerStay.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<PlayerInput>())
        {
            _players.Remove(other.gameObject);
            OnPlayerExit.Invoke();
        }
    }


    void OnDrawGizmos()
    {
        var box = GetComponent<BoxCollider>();
        Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
        Gizmos.DrawCube(transform.position,
            new Vector3(transform.localScale.x * box.size.x, transform.localScale.y * box.size.y,
                transform.localScale.z * box.size.z));


        Gizmos.color = new Color(1f, 0f, 0f, 1f);
        Gizmos.DrawWireCube(transform.position,
            new Vector3(transform.localScale.x * box.size.x, transform.localScale.y * box.size.y,
                transform.localScale.z * box.size.z));
    }
}