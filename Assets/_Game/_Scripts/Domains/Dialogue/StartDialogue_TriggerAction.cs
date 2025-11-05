using Game.Core.Dialogue;
using Game.Core.Trigger;
using UnityEngine;
using VContainer;

namespace Game.Domains.Dialogue
{
    public sealed class StartDialogue_TriggerAction : BaseTriggerAction
    {
        [SerializeField] private DialogueData _dialogueData;
        private IDialogueManager _dialogueManager;

        public override void Inject(GameObject triggerGO, IObjectResolver objectResolver)
        {
            _dialogueManager = objectResolver.Resolve<IDialogueManager>();
            base.Inject(triggerGO, objectResolver);
        }

        protected override void OnTriggered()
        {
            _dialogueManager.StartDialogue(_dialogueData);
        }
    }
}
