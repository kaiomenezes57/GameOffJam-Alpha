using Game.Core.Dialogue;
using Game.Core.Trigger;
using UnityEngine;
using VContainer;

namespace Game.Domains.Trigger
{
    public sealed class StartDialogue_TriggerAction : BaseTriggerAction
    {
        [SerializeField] private DialogueData _dialogueData;
        private IDialogueManager _dialogueManager;

        public override void Inject(IObjectResolver objectResolver)
        {
            _dialogueManager = objectResolver.Resolve<IDialogueManager>();
            base.Inject(objectResolver);
        }

        protected override void OnTriggered()
        {
            _dialogueManager.StartDialogue(_dialogueData);
        }
    }
}
