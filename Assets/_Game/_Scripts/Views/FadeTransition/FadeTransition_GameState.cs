using Game.Core.GameState;
using Game.Core.StateMachine;

namespace Game.Views.FadeTransition
{
    public sealed class FadeTransition_GameState : BaseGameState
    {
        public override bool PlayerActive => false;
        public override bool ShowMouse => false;

        public override IState[] InvalidNextStates { get; } = new IState[] {
            new Phone_GameState(),
        };


        public override void Enter(IStateMachine stateMachine)
        {
        }
    }
}
