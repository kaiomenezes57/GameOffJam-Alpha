using Game.Core.StateMachine;

namespace Game.Core.GameState
{
    public interface IGameState : IState
    {
        bool PlayerActive { get; }
        bool ShowMouse { get; }
    }
}
