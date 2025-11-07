using Game.Core.Dialogue;
using Game.Core.Interaction;
using Game.Core.Telephone;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace Game.Domains.Telephone
{
    public sealed class TelephoneInteractionStarter : BaseInteractable, ITelephone
    {
        [Inject] private readonly IDialogueManager _dialogueManager;
        [SerializeField] private UnityEvent _onStartRingining;
        private DialogueData _currentDialogueData;

        protected override void OnInteract()
        {
            _dialogueManager.StartDialogue(_currentDialogueData);
            _currentDialogueData = default;
        }

        public override bool CanInteract()
        {
            return !_currentDialogueData.Equals(default(DialogueData)) &&
                _currentDialogueData.IsValid();
        }

        public void Ring(DialogueData dialogueData)
        {
            if (!dialogueData.IsValid())
                return;

            _currentDialogueData = dialogueData;
            _onStartRingining?.Invoke();
        }
    }
}