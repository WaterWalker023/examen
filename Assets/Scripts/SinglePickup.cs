using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SinglePickup : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public bool Interact(PlayerPickup interactor, GameObject player)
    {
        transform.SetParent(player.transform);
        transform.position = interactor.carryPoint.transform.position;
        transform.rotation = new Quaternion(0, 0, 0,0);
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        return true;
    }

    public bool PutDown(PlayerPickup interactor,GameObject player)
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        transform.parent = null;
        _rigidbody.AddForce(player.transform.up*400);
        _rigidbody.AddForce(player.transform.forward*600);
        return true;
    }
}
