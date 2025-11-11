using Game.Core.Interaction;

namespace Game.Core.Events
{
    public sealed class OnUpdateInteraction : IGameEvent
    {
        public IInteractable Interactable { get; }

        public OnUpdateInteraction(IInteractable interactable)
        {
            Interactable = interactable;
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
