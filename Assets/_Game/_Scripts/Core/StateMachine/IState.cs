namespace Game.Core.StateMachine
{
    public interface IState
    {
        void Enter(IStateMachine stateMachine);
        void Tick(IStateMachine stateMachine);
        void Exit(IStateMachine stateMachine);
    }
}
