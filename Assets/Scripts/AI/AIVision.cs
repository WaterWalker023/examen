using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class AIVision : MonoBehaviour
{
    [SerializeField] float visionRange;
    [SerializeField] LayerMask visionLayer;
    [SerializeField] float timeToSeePlayer;
    
    private float _visibletime;

    [SerializeField] private UnityEvent firstspot;
    private int _playersvisible;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Scan();
        
        if (_playersvisible == 0) {_visibletime = 0; return; }
        
        SeeingPlayer();
    }

    public bool IsInSight(GameObject obj)
    {
        Physics.Raycast(transform.position, obj.transform.position - transform.position, out RaycastHit hit, visionRange, visionLayer);
        if (hit.collider.transform == obj.transform)
        {
            Debug.DrawRay(transform.position, obj.transform.position - transform.position, Color.red);
            return true;
        }
        return false;
    }

    private void Scan()
    {
        _playersvisible = 0;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, visionRange, visionLayer, QueryTriggerInteraction.Collide);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (!hitColliders[i].CompareTag("Player")) continue;
            
            GameObject obj = hitColliders[i].gameObject;
            
            if (IsInSight(obj))
            {
               _playersvisible++;
            }
        }
    }

    private void SeeingPlayer()
    {
        _visibletime += (Time.deltaTime * _playersvisible);
        if (_visibletime > timeToSeePlayer)
        {
            firstspot.Invoke();
        }
    }
    
}
