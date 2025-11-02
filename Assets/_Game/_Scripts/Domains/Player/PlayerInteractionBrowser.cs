using UnityEngine.InputSystem;
using UnityEngine;
using Game.Core.Interaction;
using Game.Core.Extensions;
using Game.Core.Utilities.DisablableComponent;

namespace Game.Domains.Player
{
    public sealed class PlayerInteractionBrowser : BaseDisablableComponent
    {
        [SerializeField] private InputActionReference _interactInput;
        [SerializeField] private InputActionReference _mousePosition;
        private Camera _camera;

        private IInteractable Current
        {
            get => _current;
            set
            {
                if (_current == value)
                    return;
                _current = value;
            }
        }
        private IInteractable _current;

        private void OnEnable()
        {
            _interactInput.action.Enable();
            _interactInput.action.performed += Interact;
            
            _mousePosition.action.Enable();
        }

        private void OnDisable()
        {
            _interactInput.action.Disable();
            _interactInput.action.performed -= Interact;

            _mousePosition.action.Disable();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!Enabled)
                return;

            var mousePosition = _mousePosition.action.ReadValue<Vector2>();
            var ray = _camera.ViewportPointToRay(mousePosition);

            bool foundAnything = 
                Physics.Raycast(ray, out var hitInfo, 2f) &&
                hitInfo.collider != null;

            if (!foundAnything)
                return;
            
            var gameObject = hitInfo.collider.gameObject;
            var interactable = gameObject.GetComponentAnywhere<IInteractable>();
            var isValid = interactable != null && interactable.CanInteract();
            
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