using Game.Core.Events;
using Game.Core.Extensions;
using Game.Core.GameState;
using Game.Core.StateMachine;
using System.Collections.Generic;

namespace Game.Application.GameState
{
    public sealed class GameStateHandler : BaseStateMachine, IGameStateHandler
    {
        private readonly Dictionary<object, IGameState> _previousGameStates = new();

        public bool TryChange(IGameState state, object caller)
        {
            if (Current != null && !Current.IsValidAsNextState(state))
                return false;

            _previousGameStates[caller] = Current as IGameState;
            ChangeState(state);

            EventBus.Raise(new OnChangeGameState(state));
            return true;
        }

        public void BackToPrevious(object caller)
        {
            if (!_previousGameStates.ContainsKey(caller))
                return;

            var state = _previousGameStates[caller];
            if (state == null)
                return;
            
            ChangeState(state);

            _previousGameStates.Remove(caller);
            EventBus.Raise(new OnChangeGameState(state));
        }
    }
}
