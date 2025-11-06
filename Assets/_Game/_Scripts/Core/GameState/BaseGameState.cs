using Game.Core.StateMachine;

namespace Game.Core.GameState
{
    public abstract class BaseGameState : IGameState
    {
        public abstract bool PlayerActive { get; }
        public abstract bool ShowMouse { get; }
        public virtual IState[] CompatibleNextStates { get; }

        public abstract void Enter(IStateMachine stateMachine);
        public virtual void Tick(IStateMachine stateMachine) { }
        public virtual void Exit(IStateMachine stateMachine) { }
    }
}
