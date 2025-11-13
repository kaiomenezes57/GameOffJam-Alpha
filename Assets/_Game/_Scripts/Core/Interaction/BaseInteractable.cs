using DG.Tweening;
using Game.Core.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Core.Interaction
{
    public abstract class BaseInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool _disableOnPlayerDisable = true;
        [SerializeField] private UnityEvent _onInteract;
        private Collider _collider;
        private bool _isInCooldown;

        private void OnEnable()
        {
            EventBus.Subscribe<OnChangeGameState>(SwitchEnable);
        }

        private void OnDisable()
        {
            EventBus.UnSubscribe<OnChangeGameState>(SwitchEnable);
        }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            //gameObject.layer = 3;
        }

        private void SwitchEnable(OnChangeGameState state)
        {
            if (!_disableOnPlayerDisable)
                return;
            _collider.enabled = state.GameState.PlayerActive;
        }

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
        public virtual bool CanInteract()
        {
            return true;
        }
    }
}