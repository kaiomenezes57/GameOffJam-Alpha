namespace Game.Core.GameState
{
    public interface IGameStateHandler
    {
        bool TryChange(IGameState state, object caller);
        void BackToPrevious(object caller);
    }
}