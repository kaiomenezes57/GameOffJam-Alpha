using Game.Core.Events;
using Game.Core.GameState;
using Game.Core.StateMachine;
using System.Collections.Generic;

namespace Game.Domains.GameState
{
    public sealed class GameStateHandler : BaseStateMachine, IGameStateHandler
    {
        private readonly Dictionary<object, IGameState> _previousGameStates = new();

        public void Change(IGameState state, object caller)
        {
            if (_previousGameStates.ContainsKey(caller))
                return;
            
            _previousGameStates[caller] = Current as IGameState;
            Change(state);

            EventBus.Raise(new OnChangeGameState(state));
        }

        public void BackToPrevious(object caller)
        {
            if (!_previousGameStates.ContainsKey(caller))
                return;

            var state = _previousGameStates[caller];
            if (state == null)
                return;
            
            Change(state);
            EventBus.Raise(new OnChangeGameState(state));
        }
    }
}
