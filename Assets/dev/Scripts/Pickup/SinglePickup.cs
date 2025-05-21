using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SinglePickup : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    private Rigidbody _rigidbody;

    private ParticleSystem _vfx;

    private bool _isBeingHeld;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _vfx = GetComponentInChildren<ParticleSystem>();
    }

    public bool Interact(PlayerPickup interactor, GameObject player)
    {
        _vfx.gameObject.SetActive(false);
        transform.SetParent(player.transform);
        transform.position = interactor.carryPoint.transform.position;
        transform.rotation = new Quaternion(0, 0, 0,0);
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _isBeingHeld = true;
        return true;
    }

    public bool PutDown(PlayerPickup interactor,GameObject player)
    {
        _vfx.gameObject.SetActive(true);
        _rigidbody.constraints = RigidbodyConstraints.None;
        transform.parent = null;
        _rigidbody.AddForce(player.transform.up*interactor.upThrowForce);
        _rigidbody.AddForce(player.transform.forward*interactor.horizontalThrowForce);
        _isBeingHeld = false;
        return true;
    }

    private void OnDestroy()
    {
        if (_isBeingHeld)
        {
            transform.parent.GetComponent<PlayerPickup>().PickupGone();
        }
    }
}
