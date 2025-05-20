using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerPickup : MonoBehaviour
{
    private InputActionAsset _inputActionAsset;
    private InputActionMap _playerInputMap;
    private InputAction _interactAction;
    private InputAction _throwAction;
    
    [SerializeField] private Transform startCapsule;
    [SerializeField] private Transform endCapsule;
    [SerializeField] private float radiusCapsule;
    [SerializeField] private LayerMask overlapLayer;
    
    public int upThrowForce;
    public int horizontalThrowForce;
    
    public GameObject carryPoint;
    
    private readonly Collider[] _colliders = new Collider[5];
    private int _numFound;
    private bool _isHoldingObject;
    private GameObject _holdingObject;

    public bool havePickedUpIngredient
    {
        get
        {
            return _isHoldingObject;
        }
    }
    void Start()
    {
        _inputActionAsset = GetComponent<PlayerInput>().actions;
        _playerInputMap = _inputActionAsset.FindActionMap("PLayer");
        
        _interactAction = _playerInputMap.FindAction("Interact");
        _throwAction = _playerInputMap.FindAction("Throw");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHoldingObject && _throwAction.WasPerformedThisFrame())
        {
            var interactable = _holdingObject.GetComponent<IInteractable>();
            interactable.PutDown(this, gameObject);
            _isHoldingObject = false;
            _holdingObject = null;
        }
        
        _numFound = Physics.OverlapCapsuleNonAlloc(startCapsule.transform.position,endCapsule.transform.position,radiusCapsule,_colliders,overlapLayer);
        if (_numFound > 0 && !_holdingObject)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();
            if (interactable != null && _interactAction.WasPressedThisFrame())
            {
                interactable.Interact(this, gameObject);
                _isHoldingObject = true;
                _holdingObject = _colliders[0].gameObject;
            }
        }
        
    }

    public void PickupGone()
    {
        _isHoldingObject = false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(startCapsule.transform.position,radiusCapsule);
        Gizmos.DrawWireSphere(endCapsule.transform.position,radiusCapsule);
    }
}
