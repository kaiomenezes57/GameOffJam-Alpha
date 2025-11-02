using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Core.Extensions
{
    public static class CollectionsExtensions
    {
        public static T GetRandom<T>(this ICollection<T> source) where T : class
        {
            if (source is not { Count: > 0 }) 
                return default;

            var randomIndex = Random.Range(0, source.Count);
            return source.ToArray()[randomIndex];
        }
    }
}
