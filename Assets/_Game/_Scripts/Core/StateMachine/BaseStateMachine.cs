namespace Game.Core.StateMachine
{
    public abstract class BaseStateMachine : IStateMachine
    {
        public IState Current { get; private set; }

        public void Change(IState state)
        {
            Current?.Exit(this);
            
            Current = state;
            Current?.Enter(this);
        }

        private void Tick()
        {
            Current?.Tick(this);
        }

        public void Dispose()
        {
            Current?.Exit(this);
        }
    }
}