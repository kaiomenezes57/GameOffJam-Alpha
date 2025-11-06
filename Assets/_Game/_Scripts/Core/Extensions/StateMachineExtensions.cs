using Game.Core.StateMachine;

namespace Game.Core.Extensions
{
    public static class StateMachineExtensions
    {
        public static bool IsValidAsNextState(this IState source, IState nextState)
        {
            if (source.GetType() == nextState.GetType())
                return false;

            var compatibleStates = source.CompatibleNextStates;
            if (compatibleStates == null || compatibleStates.Length == 0)
                return true;

            foreach (var compatible in compatibleStates)
            {
                if (compatible.GetType() == nextState.GetType())
                    return true;
            }

            return false;
        }
    }
}
