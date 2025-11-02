using Game.Core.Events;
using UnityEngine;

namespace Game.Core.Utilities.Mouse
{
    public sealed class MouseSetter : MonoBehaviour
    {
        private void OnEnable()
        {
            EventBus.Subscribe<OnChangeGameState>(Switch);
        }

        private void OnDisable()
        {
            EventBus.Subscribe<OnChangeGameState>(Switch);
        }

        private void Switch(OnChangeGameState state)
        {
            bool showMouse = state.GameState.ShowMouse;
            Cursor.lockState = showMouse ?
                CursorLockMode.Confined :
                CursorLockMode.Locked;
            Cursor.visible = showMouse;
        }
    }
}
