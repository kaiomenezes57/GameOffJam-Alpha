using Game.Core.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.Crosshair
{
    public sealed class CrosshairSwitcher : MonoBehaviour
    {
        [SerializeField] private Image _crosshair;

        private void OnEnable()
        {
            EventBus.Subscribe<OnChangeGameState>(Switch);
        }

        private void OnDisable()
        {
            EventBus.UnSubscribe<OnChangeGameState>(Switch);
        }

        private void Switch(OnChangeGameState state)
        {
            _crosshair.enabled = state.GameState.PlayerActive;
        }
    }
}
