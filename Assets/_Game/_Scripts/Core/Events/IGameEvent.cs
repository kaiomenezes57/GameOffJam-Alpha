using Game.Core.Utilities;

namespace Game.Core.Events
{
    public interface IGameEvent : IValidator
    {

    }

    public sealed class RequestDialogue_GameEvent : IGameEvent
    {
        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed class NextDialogue_GameEvent : IGameEvent
    {
        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed class EndDialogue_GameEvent : IGameEvent
    {
        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
