using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Core.FadeTransition;
using Game.Core.GameState;
using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Views.FadeTransition
{
    /// <summary>
    /// Soft fade transitions with event calling.
    /// </summary>
    public sealed class FadeTransitionUI : MonoBehaviour, IFadeTransition
    {
        [Inject] private readonly IGameStateHandler _gameStateHandler;
        [SerializeField] private Image _blackImage;

        private void Start()
        {
            Color color = _blackImage.color;
            color.a = 0f;
            _blackImage.color = color;
        }

        public async void Perform(float blackScreenDuration,
            float fadeDuration = 0.5f,
            Action onFadeInStarted = null, 
            Action onFadeInCompleted = null, 
            Action onFadeOutCompleted = null)
        {
            blackScreenDuration = Mathf.Clamp(blackScreenDuration, 0f, float.PositiveInfinity);
            fadeDuration = Mathf.Clamp(fadeDuration, 0f, float.PositiveInfinity);

            await FadeOut(fadeDuration, onFadeOutCompleted);

            if (await UniTask.WaitForSeconds(blackScreenDuration,
                cancellationToken: destroyCancellationToken)
                .SuppressCancellationThrow())
                return;

            await FadeIn(fadeDuration, onFadeInStarted, onFadeInCompleted);
        }

        private async UniTask FadeOut(float fadeDuration, Action onFadeOutCompleted)
        {
            _gameStateHandler.TryChange(new FadeTransition_GameState(), this);

            await _blackImage.DOFade(1f, fadeDuration)
             .SetUpdate(true)
             .AsyncWaitForCompletion();
            
            onFadeOutCompleted?.Invoke();
        }

        private async UniTask FadeIn(float fadeDuration, Action onFadeInStarted, Action onFadeInCompleted)
        {
            onFadeInStarted?.Invoke();

            await _blackImage.DOFade(0f, fadeDuration)
                .SetUpdate(true)
                .AsyncWaitForCompletion();

            _gameStateHandler.BackToPrevious(this);
            onFadeInCompleted?.Invoke();
        }
    }
}
