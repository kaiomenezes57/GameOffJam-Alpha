using Game.Core.Events;
using Game.Core.GameState;

namespace Game.Domains.GameState
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
