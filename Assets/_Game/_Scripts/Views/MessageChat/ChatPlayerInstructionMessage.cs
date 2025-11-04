using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using DG.Tweening;

namespace Game.Views.MessageChat
{
    public sealed class ChatPlayerInstructionMessage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _instructionMessage;
        [SerializeField] private LocalizedString _confirmationInstruction;
        [SerializeField] private LocalizedString _typeInstruction;
        private const float INSTRUCTION_SIZE_MULTIPLIER = 1.05f;
        private Sequence _sequence;

        public void Show(bool type)
        {
            _instructionMessage.text = type ? 
                _typeInstruction.GetLocalizedString() : 
                _confirmationInstruction.GetLocalizedString();
            _instructionMessage.gameObject.SetActive(true);

            _sequence = DOTween.Sequence()
                .Append(_instructionMessage.transform.DOScale(Vector3.one * INSTRUCTION_SIZE_MULTIPLIER, 0.5f))
                .Append(_instructionMessage.transform.DOScale(Vector3.one, 0.5f))
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void Hide()
        {
            _instructionMessage.transform.localScale = Vector3.one;
            _instructionMessage.gameObject.SetActive(false);
            _sequence?.Kill();
        }
    }
}
