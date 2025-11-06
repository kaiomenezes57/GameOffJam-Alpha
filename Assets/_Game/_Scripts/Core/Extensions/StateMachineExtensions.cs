using Game.Core.StateMachine;

namespace Game.Core.Extensions
{
    public static class StateMachineExtensions
    {
        public static bool IsValidAsNextState(this IState source, IState nexState)
        {
            if (source.GetType() == nexState.GetType())
                return false;

            var invalidStates = source.InvalidNextStates;
            if (invalidStates == null || invalidStates.Length == 0)
                return true;

            foreach (var invalid in invalidStates)
            {
                if (invalid.GetType() == nexState.GetType())
                    return false;
            }

            return true;
        }
    }
}
