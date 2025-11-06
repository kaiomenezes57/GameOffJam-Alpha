namespace Game.Core.GameState
{
    public sealed class Phone_GameState : BaseGameState
    {
        public override bool PlayerActive => false;
        public override bool ShowMouse => true;
    }
}