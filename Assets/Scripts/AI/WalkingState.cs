using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class WalkingState : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Vector4 randomrange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance <= 1)
        {
            _agent.SetDestination(new Vector3(Random.Range(randomrange.x, randomrange.y + 1), 0,
                Random.Range(randomrange.z, randomrange.w + 1)));
        }
    }
}