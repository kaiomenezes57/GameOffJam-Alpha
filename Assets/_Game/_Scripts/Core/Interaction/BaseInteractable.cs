using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Core.Interaction
{
    public abstract class BaseInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent _onInteract;
        private bool _isInCooldown;

        public void Interact()
        {
            if (!HandleCooldown())
                return;
            
            _onInteract?.Invoke();
            OnInteract();
        }

        private bool HandleCooldown()
        {
            if (_isInCooldown) return false;
            _isInCooldown = true;

            DOVirtual.DelayedCall(1f, () => _isInCooldown = false);
            return true;
        }

        protected abstract void OnInteract();
        public abstract bool CanInteract();
    }
}