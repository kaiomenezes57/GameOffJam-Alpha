using Game.Core.StateMachine;

namespace Game.Core.GameState
{
    public abstract class BaseGameState : IGameState
    {
        public abstract bool PlayerActive { get; }
        public abstract bool ShowMouse { get; }
        public virtual IState[] InvalidNextStates { get; }
        public IState NextState => throw new System.NotImplementedException();

        public virtual void Enter(IStateMachine stateMachine) { }
        public virtual void Tick(IStateMachine stateMachine, float deltaTime) { }
        public virtual void Exit(IStateMachine stateMachine) { }
    }
}
