using UnityEngine;

namespace Game.Core.Extensions
{
    public static class GameObjectExtensions
    {
        public static T GetOrAdd<T>(this GameObject source) where T : Component
        {
            if (!source.TryGetComponent(out T component))
                component = source.AddComponent<T>();
            return component;
        }

        public static bool TryGetOrAdd<T>(this GameObject source, out T component) where T : Component
        {
            component = GetOrAdd<T>(source);
            return component != null;
        }
    }
}
