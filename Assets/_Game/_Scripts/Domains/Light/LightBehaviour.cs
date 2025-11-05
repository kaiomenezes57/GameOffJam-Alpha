using DG.Tweening;
using Game.Core.Light;
using UnityEngine;

namespace Game.Domains.Light
{
    public sealed class LightBehaviour : MonoBehaviour, ILightBehaviour
    {
        [SerializeField] private bool _startTurnedOn = true;
        private const float ANIMATION_DURATION = 1f;

        private UnityEngine.Light _light;
        private float _initialIntensity;
        private bool _canSwitch;

        private void Start()
        {
            _light = GetComponent<UnityEngine.Light>();

            _initialIntensity = _light.intensity;
            _light.intensity = _startTurnedOn ? _initialIntensity : 0f;

            _canSwitch = true;
        }

        private void OnDisable()
        {
            _light.DOKill();
        }

        public void TurnOn()
        {
            Animate(intensity: _initialIntensity);
        }

        public void TurnOff()
        {
            Animate(intensity: 0f);
        }

        private void Animate(float intensity)
        {
            if (!_canSwitch) return;
            _canSwitch = false;

            _light.DOIntensity(intensity, ANIMATION_DURATION)
                 .OnComplete(() => _canSwitch = true)
                 .SetLink(gameObject);
        }
    }
}
