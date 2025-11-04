using UnityEngine.InputSystem;
using Game.Core.MessageChat;
using UnityEngine;
using System;
using TMPro;
using Sirenix.OdinInspector;

namespace Game.Views.MessageChat
{
    public sealed class PlayerInputChatMessageViewUI : MonoBehaviour, IPlayerInputChatMessageViewUI
    {
        [Title("References")]
        [SerializeField] private TextMeshProUGUI _playerMessage;
        [SerializeField] private InputActionReference _confirmInput;
        [SerializeField] private InputActionReference _typingInput;
        [SerializeField] private ChatPlayerInstructionMessage _instructionMessage;

        private Action _currentOnComplete;
        private bool _canConfirm;

        private void OnEnable()
        {
            _confirmInput.action.Enable();
            _confirmInput.action.performed += Confirm;
        }

        private void OnDisable()
        {
            _confirmInput.action.Disable();
            _confirmInput.action.performed -= Confirm;

            _typingInput.action.Disable();
            _typingInput.action.performed -= OnTypingPerformed;
        }

        private void Start()
        {
            _instructionMessage.Hide();
        }

        public void StartTyping(string message, Action onComplete)
        {
            _playerMessage.text = message;
            _playerMessage.maxVisibleCharacters = 0;

            _currentOnComplete = onComplete;
            _canConfirm = false;

            _typingInput.action.Enable();
            _typingInput.action.performed += OnTypingPerformed;

            _instructionMessage.Show(type: true);
        }

        private void OnTypingPerformed(InputAction.CallbackContext ctx)
        {
            _playerMessage.maxVisibleCharacters++;
            _instructionMessage.Hide();

            if (_playerMessage.maxVisibleCharacters >= _playerMessage.text.Length)
            {
                _typingInput.action.Disable();
                _typingInput.action.performed -= OnTypingPerformed;
                _instructionMessage.Show(type: false);
                _canConfirm = true;
            }
        }

        private void Confirm(InputAction.CallbackContext ctx)
        {
            if (!_canConfirm)
                return;

            _playerMessage.text = string.Empty;
            _currentOnComplete?.Invoke();
            _currentOnComplete = null;
            
            _instructionMessage.Hide();
        }
    }
}
