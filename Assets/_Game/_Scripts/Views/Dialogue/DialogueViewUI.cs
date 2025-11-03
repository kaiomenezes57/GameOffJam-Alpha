using DG.Tweening;
using Game.Core.Dialogue;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.Dialogue
{
    public sealed class DialogueViewUI : MonoBehaviour, IDialogueViewUI
    {
        [SerializeField] private Button _nextLineButton;
        [SerializeField] private TextMeshProUGUI _characterName;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        public event Action OnNextDialogueRequested;

        private const float BUTTON_INTERACTABLE_COOLDOWN = 3f;
        private const float FADE_DURATION = 1f;
        private bool _useButton;

        private void OnEnable()
        {
            _nextLineButton.onClick.AddListener(RequestNextLine);
        }

        private void OnDisable()
        {
            _nextLineButton.onClick.RemoveListener(RequestNextLine);
        }

        private void Start()
        {
            _characterName.alpha = 0f;
            _dialogueText.alpha = 0f;

            _characterName.text = string.Empty;
            _dialogueText.text = string.Empty;

            _nextLineButton.gameObject.SetActive(false);
        }

        public void ShowLine(string characterName, string dialogueText)
        {
            _dialogueText.alpha = 0f;

            _characterName.text = characterName;
            _dialogueText.text = dialogueText;

            SwitchButtonInteractable();

            if (_characterName.alpha < 1f)
                _characterName.DOFade(1f, FADE_DURATION);
         
            _dialogueText.DOFade(1f, FADE_DURATION);
        }

        public void Hide()
        {
            _characterName.DOFade(0f, FADE_DURATION)
                .OnComplete(() => _characterName.text = string.Empty);
            _dialogueText.DOFade(0f, FADE_DURATION)
                .OnComplete(() => _dialogueText.text = string.Empty);
            
            _nextLineButton.gameObject.SetActive(false);
        }

        private void SwitchButtonInteractable()
        {
            if (!_useButton)
                return;

            _nextLineButton.gameObject.SetActive(false);
            DOVirtual.DelayedCall(BUTTON_INTERACTABLE_COOLDOWN,
                () => _nextLineButton.gameObject.SetActive(true))
                .SetLink(gameObject);
        }

        private void RequestNextLine()
        {
            OnNextDialogueRequested?.Invoke();
        }

        public void SetNextDialogueButton(bool active)
        {
            _useButton = active;
        }
    }
}
