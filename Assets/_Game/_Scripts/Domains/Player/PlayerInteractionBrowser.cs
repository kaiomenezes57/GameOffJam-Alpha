using UnityEngine.InputSystem;
using UnityEngine;
using Game.Core.Interaction;
using Game.Core.Utilities.DisablableComponent;
using Game.Core.Events;

namespace Game.Domains.Player
{
    public sealed class PlayerInteractionBrowser : BaseDisablableComponent
    {
        [SerializeField] private InputActionReference _interactInput;
        private const float INTERACTION_DISTANCE = 1.5f;
        private Camera _camera;

        private IInteractable Current
        {
            get => _current;
            set
            {
                if (_current == value)
                    return;
                _current = value;
                EventBus.Raise(new OnUpdateInteraction(_current));
            }
        }
        private IInteractable _current;

        private void OnEnable()
        {
            _interactInput.action.Enable();
            _interactInput.action.performed += Interact;
        }

        private void OnDisable()
        {
            _interactInput.action.Disable();
            _interactInput.action.performed -= Interact;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            //if (!Enabled)
            //    return;

            var mousePosition = Mouse.current.position.ReadValue();
            var ray = _camera.ScreenPointToRay(mousePosition);

#if DEBUG
            Debug.DrawRay(ray.origin, ray.direction * INTERACTION_DISTANCE, Color.red);
#endif

            bool foundAnything = 
                Physics.Raycast(ray, out var hitInfo, INTERACTION_DISTANCE) &&
                hitInfo.collider != null;

            if (!foundAnything)
            {
                Current = null;
                return;
            }
            
            var gameObject = hitInfo.collider.gameObject;
            var interactable = gameObject.GetComponent<IInteractable>();
            var isValid = interactable != null && 
                (interactable as MonoBehaviour).enabled && 
                interactable.CanInteract();

#if DEBUG
            Debug.DrawRay(ray.origin, ray.direction * INTERACTION_DISTANCE, Color.green);
#endif

            Current = isValid ? interactable : null;
        }

        private void Interact(InputAction.CallbackContext ctx)
        {
            if (Current == null || !Current.CanInteract())
                return;
            Current.Interact();
            Current = null;
        }
    }
}