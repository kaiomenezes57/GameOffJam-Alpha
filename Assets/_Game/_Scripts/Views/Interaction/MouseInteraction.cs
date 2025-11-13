using DG.Tweening;
using Game.Core.Interaction;
using UnityEngine;

namespace Game.Views.Interaction
{
    public sealed class MouseInteraction : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEngine.Events.UnityEvent _onInteract;
        private bool _isInCooldown = false;

        public void Interact()
        {
            if (!HandleCooldown())
                return;
            _onInteract?.Invoke();
        }

        private bool HandleCooldown()
        {
            if (_isInCooldown) return false;
            _isInCooldown = true;

            DOVirtual.DelayedCall(1f, () => _isInCooldown = false);
            return true;
        }

        public bool CanInteract()
        {
            return true;
        }

        public void OnMouseDown()
        {
            if (CanInteract())
            {
                Interact();
            }
        }
    }
}
