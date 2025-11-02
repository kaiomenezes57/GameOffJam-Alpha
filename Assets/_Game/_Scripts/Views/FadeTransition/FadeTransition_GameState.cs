using Game.Core.GameState;
using Game.Core.StateMachine;

namespace Game.Views.FadeTransition
{
    public sealed class FadeTransition_GameState : BaseGameState
    {
        public override bool PlayerActive => false;
        public override bool ShowMouse => false;

        public override void Enter(IStateMachine stateMachine)
        {
        }
    }
}
