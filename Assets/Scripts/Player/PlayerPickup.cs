using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using System.Collections;
using System.Dynamic;

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
    private Animator CharacterController;
    float yVelocity = 0.0f;

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

        CharacterController = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHoldingObject && _throwAction.WasPerformedThisFrame())
        {
            CharacterController.SetTrigger("Throw");
            var interactable = _holdingObject.GetComponent<IInteractable>();
            StartCoroutine(DelayAction(1, interactable));
           
            _isHoldingObject = false;
            _holdingObject = null;
        }
        var m_currentLayerWeight = CharacterController.GetLayerWeight(1);
        m_currentLayerWeight = Mathf.SmoothDamp(m_currentLayerWeight, _isHoldingObject ? 1 : 0, ref yVelocity, 2f);
        CharacterController.SetLayerWeight(1, m_currentLayerWeight);

        _numFound = Physics.OverlapCapsuleNonAlloc(startCapsule.transform.position, endCapsule.transform.position, radiusCapsule, _colliders, overlapLayer);
        if (_numFound > 0 && !_holdingObject)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();
            if (interactable != null && _interactAction.WasPressedThisFrame())
            {
                interactable.Interact(this, gameObject);
                _isHoldingObject = true;
                _holdingObject = _colliders[0].gameObject;
                CharacterController.SetLayerWeight(1, 1);
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
        Gizmos.DrawWireSphere(startCapsule.transform.position, radiusCapsule);
        Gizmos.DrawWireSphere(endCapsule.transform.position, radiusCapsule);
    }

    IEnumerator DelayAction(float Time, IInteractable interactable)
    {
        yield return new WaitForSeconds(Time);
        interactable.PutDown(this, gameObject);
    }

}
