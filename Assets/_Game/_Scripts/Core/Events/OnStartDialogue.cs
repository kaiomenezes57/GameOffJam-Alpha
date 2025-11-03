using Game.Core.Dialogue;

namespace Game.Core.Events
{
    public sealed class OnStartDialogue : IGameEvent
    {
        public DialogueData DialogueData { get; }

        public OnStartDialogue(DialogueData dialogueData)
        {
            DialogueData = dialogueData;
        }

        public bool IsValid()
        {
            return DialogueData.IsValid();
        }
    }
}
