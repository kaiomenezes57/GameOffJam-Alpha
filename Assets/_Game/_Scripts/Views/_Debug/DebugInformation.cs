using Game.Core.GameState;
using Game.Core.StateMachine;
using System;
using UnityEngine;
using VContainer;

namespace Game.Views.Debug
{
    public sealed class DebugInformation : MonoBehaviour
    {
        [Inject] private readonly IGameStateHandler _gameStateHandler;
        private IStateMachine _gameStateHandlerMachine;
        private string _gameTime;

        private void Start()
        {
#if !DEBUG
            Destroy(gameObject);
#endif
            _gameStateHandlerMachine = _gameStateHandler as IStateMachine;
            _gameTime = string.Empty;
        }

        private void OnGUI()
        {
            GUI.skin.label.margin = new RectOffset(0, 0, 0, 0);
            GUI.skin.label.padding = new RectOffset(0, 0, 0, 0);
            GUI.skin.label.fontSize = 15;

            _gameTime = $"{TimeSpan.FromSeconds(Time.time):hh\\:mm\\:ss}";

            float areaWidth = 300f;  // largura do bloco
            float areaHeight = 200f; // altura do bloco
            float xPos = Screen.width - areaWidth - 10f; // 10px da borda direita

            GUILayout.BeginArea(new Rect(xPos, 10, areaWidth, areaHeight));
            GUILayout.Label("STATIC 98 - DEVELOPMENT PRE-ALPHA");
            GUILayout.Label($"Current Game State: {_gameStateHandlerMachine.Current.GetType().Name}");
            GUILayout.Label($"Game Time: {_gameTime}");
            GUILayout.EndArea();
        }

        private void OnDisable()
        {
#if DEBUG
            UnityEngine.Debug.Log($"Total game time was {_gameTime}");
#endif
        }
    }
}
