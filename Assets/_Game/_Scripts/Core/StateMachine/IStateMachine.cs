namespace Game.Core.StateMachine
{
    public interface IStateMachine
    {
        IState Current { get; }
        void ChangeState(IState state);
    }
}
