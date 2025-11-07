using Game.Core.Utilities.DisablableComponent;
using UnityEngine.InputSystem;
using Game.Core.StateMachine;
using Game.Core.Smartphone;
using Game.Core.GameState;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Game.Domains.Smartphone
{
    public sealed class PhoneManager : BaseDisablableComponent, IPhoneManager
    {
        [SerializeField] private InputActionReference _pickPhoneInput;
        [SerializeField] private PhonePicker _phonePicker;

        [Inject] private readonly IPhoneScreenSelectorView _phoneScreenSelectorView;
        [Inject] private readonly IGameStateHandler _gameStateHandler;

        private IState CurrentGameState => (_gameStateHandler as IStateMachine)?.Current;
        private PhoneScreenType _currentMode;
        private bool _canPutDownPhone = true;
        private bool _isInCooldown;

        private void OnEnable()
        {
            _pickPhoneInput.action.Enable();
            _pickPhoneInput.action.performed += Switch;
        }

        private void OnDisable()
        {
            _pickPhoneInput.action.Disable();
            _pickPhoneInput.action.performed -= Switch;
        }

        private void Switch(InputAction.CallbackContext _)
        {
            if (!HandleCooldown()) 
                return;

            if (CurrentGameState is Phone_GameState)
                TryPutDownPhone();
            else
                TryPickUpPhone();
        }

        private bool HandleCooldown()
        {
            if (_isInCooldown)
                return false;

            _isInCooldown = true;
            DOVirtual.DelayedCall(1f, () => _isInCooldown = false)
                .SetLink(gameObject);
            
            return true;
        }

        private void TryPickUpPhone()
        {
            if (!_gameStateHandler.TryChange(new Phone_GameState(), this))
                return;

            _phonePicker.PickUpPhone();
            _phoneScreenSelectorView.ShowScreen(_currentMode);

            UpdateCanPutDownPhone();
        }

        private void TryPutDownPhone()
        {
            if (!_canPutDownPhone) 
                return;

            _gameStateHandler.BackToPrevious(this);
            _phonePicker.PutDownPhone(() => {
                _currentMode = PhoneScreenType.Default;
                _phoneScreenSelectorView.HideAllScreens();
            });
        }

        public void SetNextMode(PhoneScreenType nextMode)
        {
            _currentMode = nextMode;
            UpdateCanPutDownPhone();

            if (CurrentGameState is Phone_GameState)
            {
                _phoneScreenSelectorView.ShowScreen(_currentMode);
            }
        }

        private void UpdateCanPutDownPhone()
        {
            _canPutDownPhone = _currentMode == PhoneScreenType.Default;
        }

        public void SetCanPutDownPhone()
        {
            _canPutDownPhone = true;
        }
    }
}
