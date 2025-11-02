using Game.Core.Events;
using System.Linq;
using UnityEngine;

namespace Game.Core.Utilities.DisablableComponent
{
    public sealed class ComponentsSwitcher : MonoBehaviour
    {
        private IDisablableComponent[] _components;

        private void Awake()
        {
            _components = GetComponents<IDisablableComponent>();
            _components.Concat(GetComponentsInChildren<IDisablableComponent>(true));
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnChangeGameState>(SwitchAll);
        }

        private void OnDisable()
        {
            EventBus.UnSubscribe<OnChangeGameState>(SwitchAll);
        }

        private void SwitchAll(OnChangeGameState state)
        {
            foreach (var component in _components)
                component.Switch(state.GameState.PlayerActive);
        }
    }
}
