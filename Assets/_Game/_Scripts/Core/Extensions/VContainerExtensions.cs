using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Game.Core.Extensions
{
    public static class VContainerExtensions
    {
        public static void RegisterGameObjectsOfType<T>(this IContainerBuilder _, ref List<GameObject> gameObjectList) 
            where T : MonoBehaviour
        {
            gameObjectList ??= new List<GameObject>();
            var monobehaviours = Object.FindObjectsByType<T>(
                FindObjectsInactive.Include, 
                FindObjectsSortMode.None);

            foreach (var mono in monobehaviours)
            {
                if (mono.gameObject is not { } gameObject) continue;
                gameObjectList.Add(gameObject);
            }
        }
    }
}
