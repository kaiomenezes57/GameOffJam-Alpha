using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Core.Light;
using Sirenix.OdinInspector;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Domains.Light
{
    public sealed class LightBehaviour : MonoBehaviour, ILightBehaviour
    {
        [Title("Settings")]
        [SerializeField] private bool _startTurnedOn = true;
        [SerializeField] private UnityEngine.Light _light;

        private const float ANIMATION_DURATION = 0.5f;
        private CancellationTokenSource _cts;
        private float _initialIntensity;

        [Title("Unity Events")]
        [SerializeField] private UnityEvent _onTurnOn;
        [SerializeField] private UnityEvent _onTurnOff;

        private void Start()
        {
            _initialIntensity = _light.intensity;
            _light.intensity = _startTurnedOn ? _initialIntensity : 0f;
        }

        private void OnDisable()
        {
            _light.DOKill();
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public async void TurnOn(float delay)
        {
            ReCreateToken();

            if (await UniTask.WaitForSeconds(delay, cancellationToken: _cts.Token)
                .SuppressCancellationThrow())
                return;

            Animate(intensity: _initialIntensity);
            _onTurnOn?.Invoke();
        }

        public async void TurnOff(float delay)
        {
            ReCreateToken();

            if (await UniTask.WaitForSeconds(delay, cancellationToken: _cts.Token)
                 .SuppressCancellationThrow())
                return;

            Animate(intensity: 0f);
            _onTurnOff?.Invoke();
        }

        private void Animate(float intensity)
        {
            _light.DOKill();
            _light.DOIntensity(intensity, ANIMATION_DURATION)
                 .SetLink(gameObject);
        }

        private void ReCreateToken()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
        }
    }
}
