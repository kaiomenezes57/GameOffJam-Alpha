using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Core.Events
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> _listeners = new();

        public static void Raise<T>(T gameEvent) where T : IGameEvent
        {
            var type = typeof(T);

            if (!_listeners.ContainsKey(type))
                return;

            if (gameEvent == null || !gameEvent.IsValid())
            {
#if DEBUG
                Debug.LogError($"[EVENT BUS] Game event: {gameEvent} is null or not valid.");
#endif
                return;
            }

            foreach (var listener in _listeners[type])
            {
                if (listener is not Action<T> action)
                    continue;
                action(gameEvent);
            }
        }

        public static void Subscribe<T>(Action<T> action) where T : IGameEvent
        {
            var type = typeof(T);

            if (!_listeners.ContainsKey(type))
                _listeners[type] = new List<Delegate>();

            _listeners[type].Add(action);
        }

        public static void UnSubscribe<T>(Action<T> action) where T : IGameEvent
        {
            var type = typeof(T);

            if (!_listeners.ContainsKey(type))
            {
#if DEBUG
                Debug.LogError($"[EVENT BUS] Could not unsubscribe {type}, because it does not exist.");
#endif
                return;
            }

            _listeners[type].Remove(action);
        }
    }
}
