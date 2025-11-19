using System;

namespace Game.Core.StateMachine
{
    public abstract class BaseStateMachine : 
        IStateMachine, 
        IStateMachineInitializer, 
        IStateMachineTicker, 
        IDisposable
    {
        public IState Current { get; private set; }
        protected virtual IState InitialState { get; }

        public void ChangeState(IState state)
        {
            Current?.Exit(this);
            
            Current = state;
            Current?.Enter(this);
        }

        public virtual void Initialize()
        {
            if (InitialState != null)
                ChangeState(InitialState);
        }

        public virtual void Tick(float deltaTime)
        {
            Current?.Tick(this, deltaTime);
        }

        public void Dispose()
        {
            Current?.Exit(this);
        }
    }
}