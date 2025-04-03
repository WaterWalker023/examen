using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class CatAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private NavMeshSurface _surface;

    [SerializeField] private Vector4 randomrange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
        if (randomrange == new Vector4(0,0,0,0))
        {
            randomrange = new Vector4
            (
                _surface.transform.position.x - _surface.size.x,
                _surface.transform.position.x + _surface.size.x,
                _surface.transform.position.z - _surface.size.z,
                _surface.transform.position.z + _surface.size.z
            );
        }
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