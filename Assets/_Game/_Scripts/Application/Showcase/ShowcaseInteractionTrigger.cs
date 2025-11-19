using Game.Core.Extensions;
using Game.Core.GameState;
using Game.Core.Interaction;
using Game.Core.Showcase;
using Game.Core.StateMachine;
using UnityEngine;
using UnityEngine.Localization;
using VContainer;

namespace Game.Application.Showcase
{
    public sealed class ShowcaseInteractionTrigger : BaseInteractable
    {
        [SerializeField] private LocalizedString _message;
        [SerializeField] private ShowcaseCamera _camera;
        
        [Inject] private readonly IGameStateHandler _gameStateHandler;
        [Inject] private readonly IShowcaseShow _showcaseShow;
        [Inject] private readonly IShowcaseEvents _events;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            if (_events != null)
                _events.OnClose += StopShowcase;
        }

        private void Start()
        {
            _events.OnClose += StopShowcase;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _events.OnClose -= StopShowcase;
        }

        protected override void OnInteract()
        {
            if (_gameStateHandler.TryChange(new Showcase_GameState(), this))
            {
                _camera.StartAnimation();

                var strArray = GetHeaderAndMessage(_message.GetLocalizedString());
                _showcaseShow.Show(strArray[0], strArray[1]);
            }
        }

        private string[] GetHeaderAndMessage(string entry)
        {
            var header = entry.GetSubstringBetween("[", "]").Trim();
            var message = entry[(entry.IndexOf("]") + 1)..].Trim();

            return new string[] { header, message };
        }

        public override bool CanInteract()
        {
            return base.CanInteract() &&
                (_gameStateHandler as IStateMachine).Current
                .IsValidAsNextState(new Showcase_GameState());
        }

        private void StopShowcase()
        {
            _camera.StopAnimation();
            _gameStateHandler.BackToPrevious(this);
        }
    }
}
