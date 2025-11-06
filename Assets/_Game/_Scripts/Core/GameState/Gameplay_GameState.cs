namespace Game.Core.GameState
{
    public sealed class Gameplay_GameState : BaseGameState
    {
        public override bool PlayerActive => true;
        public override bool ShowMouse => false;
    }
}
