namespace Game.Core.StateMachine
{
    public interface IState
    {
        IState[] InvalidNextStates { get; }
        IState NextState { get; }

        void Enter(IStateMachine stateMachine);
        void Tick(IStateMachine stateMachine, float deltaTime);
        void Exit(IStateMachine stateMachine);
    }
}
