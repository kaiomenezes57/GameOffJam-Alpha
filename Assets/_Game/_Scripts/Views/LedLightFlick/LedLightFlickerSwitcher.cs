using System.Linq;
using UnityEngine;

namespace Game.Views.LedLightFlick
{
    public sealed class LedLightFlickerSwitcher : MonoBehaviour
    {
        [SerializeField] private bool _startOn;
        private GameObject[] _lightFlickers;

        private void Awake()
        {
            _lightFlickers = GetComponentsInChildren<LedLightFlicker>(true)
                .Select(l => l.gameObject)
                .ToArray();
            Switch(_startOn);
        }

        public void Switch(bool enabled)
        {
            foreach (var lightFlicker in _lightFlickers)
                lightFlicker.SetActive(enabled);
        }
    }
}
