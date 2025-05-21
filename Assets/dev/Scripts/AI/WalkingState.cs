using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class WalkingState : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Vector4 randomrange;
    private Transform Starting;
    private Animator RatController;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Starting = transform;
        _agent = gameObject.GetComponent<NavMeshAgent>();

        RatController = GetComponentInChildren<Animator>();
        RatController.SetBool("Walking", true);
    }


    private void OnEnable()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _agent.speed = 10;
        RatController.SetBool("Walking", true);
    }
    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance <= 1)
        {
            _agent.SetDestination(new Vector3((Random.Range(randomrange.x, randomrange.y + 1) + Starting.position.x), 0,
                (Random.Range(randomrange.z, randomrange.w + 1) + Starting.position.z)));
                
            
        }
    }
}