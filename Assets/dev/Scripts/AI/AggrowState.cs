using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class AggrowState : MonoBehaviour
{
    private GameObject _target;
    private NavMeshAgent _agent;
    [SerializeField] private float sphereRadius;
    [SerializeField] private float attackDistance;
    private RaycastHit _hit;
    [SerializeField] LayerMask visionLayer;
    
    private float _noPlayerVisibleTime;
    [SerializeField] private float giveUpTime;
    private Animator RatController;
    private void OnEnable()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _agent.speed = 40;
        RatController = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _target = targetplayer();
        
        Physics.SphereCast(transform.position,sphereRadius, _target.transform.position - transform.position, out _hit, math.INFINITY);
        if (_hit.collider.transform == _target.transform)
        {
            _agent.SetDestination(_target.transform.position);
        }

        if (_hit.distance <= attackDistance && 3 == _hit.transform.gameObject.layer)
        {
            _hit.transform.position = _hit.transform.parent.position;
            if (_hit.transform.parent.GetComponentInChildren<SinglePickup>())
            {
                Destroy(_hit.transform.parent.GetComponentInChildren<SinglePickup>().gameObject);
            }
            RatController.SetTrigger("Attack");
        }
        
        if (!_agent.hasPath)
        {
            _noPlayerVisibleTime += 1 * Time.deltaTime;
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

    private GameObject targetplayer()
    {
        var allplayers = GameObject.FindGameObjectsWithTag("Player");
        var distance = 0f;
        foreach (var player in allplayers)
        {
            Physics.SphereCast(transform.position, sphereRadius, player.transform.position - transform.position, out _hit,
            math.INFINITY, visionLayer);
            if (_hit.collider.transform == player.transform && _hit.distance > distance)
            {
                distance = _hit.distance;
                _target = player.gameObject;
            }
        }
        return _target;
         
    }
}
