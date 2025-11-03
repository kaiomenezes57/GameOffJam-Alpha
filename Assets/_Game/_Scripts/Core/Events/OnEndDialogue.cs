namespace Game.Core.Events
{
    public sealed class OnEndDialogue : IGameEvent
    {
        public bool IsValid()
        {
            return true;
        }
    }
}
