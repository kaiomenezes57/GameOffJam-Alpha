using UnityEngine;

namespace Game.Core.Interaction
{
    public interface IInteractable
    {
        void Interact();
        bool CanInteract();
    }
}