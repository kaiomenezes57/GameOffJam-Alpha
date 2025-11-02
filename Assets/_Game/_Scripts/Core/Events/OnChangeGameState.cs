using Game.Core.GameState;

namespace Game.Core.Events
{
    public sealed class OnChangeGameState : IGameEvent
    {
        public IGameState GameState { get; }

        public OnChangeGameState(IGameState gameState)
        {
            GameState = gameState;
        }

        public bool IsValid()
        {
            return GameState != null;
        }
    }
}
