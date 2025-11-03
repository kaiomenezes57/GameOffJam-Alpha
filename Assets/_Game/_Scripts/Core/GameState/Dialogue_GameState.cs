using Game.Core.StateMachine;

namespace Game.Core.GameState
{
    public sealed class Dialogue_GameState : BaseGameState
    {
        public override bool PlayerActive => false;
        public override bool ShowMouse => true;

        public override void Enter(IStateMachine stateMachine)
        {
        }
    }
}
