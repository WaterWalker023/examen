using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class AggrowState : MonoBehaviour
{
    private float _distance;
    private GameObject _target;
    private NavMeshAgent _agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _distance = 0;
        
        var allplayers = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
        foreach (var player in allplayers)
        {
            Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, math.INFINITY);
            if (hit.collider.transform == player.transform && hit.distance > _distance)
            {
                Debug.Log("Hit " + hit.collider.name);
                _distance = hit.distance;
                _target = player.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_target.name);
        Physics.Raycast(transform.position, _target.transform.position - transform.position, out RaycastHit hit, math.INFINITY);
        if (hit.collider.transform == _target.transform)
        {
            _agent.SetDestination(_target.transform.position);
        }
    }
}
