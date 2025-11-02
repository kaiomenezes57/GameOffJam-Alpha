using Game.Core.StateMachine;

namespace Game.Core.GameState
{
    public sealed class Gameplay_GameState : BaseGameState
    {
        public override bool PlayerActive => true;
        public override bool ShowMouse => false;

        public override void Enter(IStateMachine stateMachine)
        {
        }
    }
}
