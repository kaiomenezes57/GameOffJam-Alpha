using Game.Core.GameState;
using Game.Core.Interaction;
using Game.Core.StateMachine;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace Game.Unity.Minigame
{
    public sealed class MinigameManager : BaseInteractable
    {
        [SerializeField] private CinemachineCamera _minigameCamera;
        [SerializeField] private int _requiredProgressToComplete;
        [SerializeField] private UnityEvent _onStartMinigame;
        [SerializeField] private UnityEvent _onEndMinigame;
        
        [Inject] private readonly IGameStateHandler _gameStateHandler;
        private int _currentProgress;
        private bool _completed;

        private bool TryStartMinigame()
        {
            if (!_gameStateHandler.TryChange(new Minigame_GameState(), this))
                return false;

            _minigameCamera.enabled = true;
            _onStartMinigame?.Invoke();
            return true;
        }

        public void ProgressMinigame()
        {
            _currentProgress++;
            if (_currentProgress >= _requiredProgressToComplete)
                EndMinigame();
        }

        private void EndMinigame()
        {
            _gameStateHandler.BackToPrevious(this);
            _minigameCamera.enabled = false;
            _onEndMinigame?.Invoke();

            _completed = true;
            _currentProgress = 0;
        }

        protected override void OnInteract()
        {
            TryStartMinigame();
        }

        public override bool CanInteract()
        {
            return base.CanInteract() &&
                (_gameStateHandler as IStateMachine).Current.GetType() != typeof(Minigame_GameState)
                && !_completed;
        }
    }

}
