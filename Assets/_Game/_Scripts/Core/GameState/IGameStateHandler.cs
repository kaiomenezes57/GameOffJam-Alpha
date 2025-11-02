namespace Game.Core.GameState
{
    public interface IGameStateHandler
    {
        void Change(IGameState state, object caller);
        void BackToPrevious(object caller);
    }
}