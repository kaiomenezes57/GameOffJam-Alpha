using System;

namespace Game.Core.Utilities
{
    public readonly struct Vec3
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public Vec3(float x, float y, float z)
        {
            X = x; Y = y; Z = z;
        }

        public static Vec3 operator +(Vec3 a, Vec3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vec3 operator *(Vec3 a, float d) => new(a.X * d, a.Y * d, a.Z * d);

        public Vec3 Normalized()
        {
            float mag = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            if (mag == 0) return new Vec3(0, 0, 0);
            return new Vec3(X / mag, Y / mag, Z / mag);
        }
    }
}
