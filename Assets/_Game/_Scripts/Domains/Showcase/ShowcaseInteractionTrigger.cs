using Game.Core.Extensions;
using Game.Core.GameState;
using Game.Core.Interaction;
using Game.Core.StateMachine;
using UnityEngine;
using VContainer;

namespace Game.Domains.Showcase
{
    public sealed class ShowcaseInteractionTrigger : BaseInteractable
    {
        [SerializeField] private ShowcaseCamera _camera;
        [Inject] private readonly IGameStateHandler _gameStateHandler;

        protected override void OnInteract()
        {
            if (_gameStateHandler.TryChange(new Showcase_GameState(), this))
                _camera.StartAnimation();
        }

        public override bool CanInteract()
        {
            return (_gameStateHandler as IStateMachine).Current
                .IsValidAsNextState(new Showcase_GameState());
        }

        public void StopShowcase()
        {
            _camera.StopAnimation();
            _gameStateHandler.BackToPrevious(this);
        }
    }
}
