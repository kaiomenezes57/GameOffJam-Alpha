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
            GUI.skin.label.fontSize = 20;

            _gameTime = $"{TimeSpan.FromSeconds(Time.time):hh\\:mm\\:ss}";

            GUILayout.BeginArea(new Rect(10, 10, 1000, 200));
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
