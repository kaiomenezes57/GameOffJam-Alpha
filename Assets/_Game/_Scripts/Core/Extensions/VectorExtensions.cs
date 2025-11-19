using Game.Core.Utilities;
using UnityEngine;

namespace Game.Core.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 WithOffset(this Vector3 source, 
            float? x = null, float? y = null, float? z = null)
        {
            var offset = new Vector3(
                x ?? 0f,
                y ?? 0f,
                z ?? 0f);
            return source + offset;
        }

        public static Vec3 ToApp(this Vector3 source)
        {
            return new Vec3(source.x, source.y, source.z);
        }

        public static Vector3 ToUnity(this Vec3 source)
        {
            return new Vector3(source.X, source.Y, source.Z);
        }
    }
}
