using Game.Core.Interaction;

namespace Game.Core.Events
{
    public sealed class OnUpdateInteraction : IGameEvent
    {
        public IInteractable Interactable { get; }
        public IInteractableInformationProvider InformationProvider { get; }

        public OnUpdateInteraction(IInteractable interactable, IInteractableInformationProvider informationProvider)
        {
            Interactable = interactable;
            InformationProvider = informationProvider;
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
