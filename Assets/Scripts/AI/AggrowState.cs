using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class AggrowState : MonoBehaviour
{
    private float _distance;
    private GameObject _target;
    private NavMeshAgent _agent;
    [SerializeField] private float sphereRadius;
    private RaycastHit Hit;
    
    private float _noPlayerVisibleTime;
    [SerializeField] private float giveUpTime;
    private void OnEnable()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _distance = 0;
        
        var allplayers = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
        foreach (var player in allplayers)
        {
            Physics.Raycast(transform.position,player.transform.position - transform.position, out Hit, math.INFINITY);
            Debug.Log(Hit.collider.name);
            if (Hit.collider.transform == player.transform && Hit.distance > _distance)
            {
                Debug.Log("Hit " + Hit.collider.name);
                _distance = Hit.distance;
                _target = player.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_target.name);
        Physics.SphereCast(transform.position,sphereRadius, _target.transform.position - transform.position, out Hit, math.INFINITY);
        if (Hit.collider.transform == _target.transform)
        {
            _agent.SetDestination(_target.transform.position);
        }

        if (!_agent.hasPath)
        {
            _noPlayerVisibleTime += 1 * Time.deltaTime;
            Debug.Log(_noPlayerVisibleTime);
            if (_noPlayerVisibleTime >= giveUpTime)
            {
                GetComponent<AIStateMachine>().DontSeePlayer();
            }
        }
        else
        {
            _noPlayerVisibleTime = 0;
        }
    }
}
