using UnityEngine;

public interface IInteractable
{
    public string InteractionPrompt { get; }

    public bool Interact(PlayerPickup interactor, GameObject player);

    public bool PutDown(PlayerPickup interactor, GameObject player);
}
