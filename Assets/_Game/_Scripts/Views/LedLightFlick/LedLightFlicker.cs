using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Views.LedLightFlick
{
    public sealed class LedLightFlicker : MonoBehaviour
    {
        [Title("Emission Colors")]
        [SerializeField] private float _emissionBoost = 3f;
        private Color _offColor = Color.black;
        private Color _onColor = Color.green;

        [Title("Light Settings")]
        [SerializeField] private float _lightIntensity = 2f;
        [SerializeField] private float _lightRadius = 0.5f;
        [SerializeField] private Vector3 _lightOffset;

        [Title("Flicker Settings")]
        [SerializeField] private bool _flick = true;
        [SerializeField, EnableIf(nameof(_flick))] private float _minInterval = 0.05f;
        [SerializeField, EnableIf(nameof(_flick))] private float _maxInterval = 0.3f;
        [SerializeField, EnableIf(nameof(_flick))] private float _flickerSpeed = 0.05f;

        private Renderer _renderer;
        private Material _material;
        private Light _pointLight;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _material = new Material(_renderer.sharedMaterial);
            _renderer.material = _material;

            _onColor = _material.GetColor("_EmissionColor");

            GameObject lightObj = new("LED_PointLight");
            lightObj.transform.SetParent(transform);
            lightObj.transform.localPosition = Vector3.zero + _lightOffset;

            _pointLight = lightObj.AddComponent<Light>();
            _pointLight.color = _onColor;
            _pointLight.type = LightType.Point;
            _pointLight.range = _lightRadius;
            _pointLight.intensity = _flick ? 0f : _lightIntensity;
            _pointLight.shadows = LightShadows.None;
        }

        private void Start()
        {
            if (_flick)
                FlickerLoop();
        }

        private void FlickerLoop()
        {
            var ledOn = Random.value > 0.5f;
            
            var targetColor = ledOn ? _onColor * _emissionBoost : _offColor;
            var targetIntensity = ledOn ? _lightIntensity : 0f;

            _material.DOColor(targetColor, "_EmissionColor", _flickerSpeed)
               .SetEase(Ease.Flash);

            _pointLight.DOIntensity(targetIntensity, _flickerSpeed)
                      .SetEase(Ease.Flash);

            var wait = Random.Range(_minInterval, _maxInterval);
            DOVirtual.DelayedCall(wait, FlickerLoop).SetLink(gameObject);
        }
    }
}
