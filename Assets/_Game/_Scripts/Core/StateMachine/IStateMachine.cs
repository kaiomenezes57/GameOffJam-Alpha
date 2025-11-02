using System;

namespace Game.Core.StateMachine
{
    public interface IStateMachine : IDisposable
    {
        IState Current { get; }
        void Change(IState state);
    }
}
