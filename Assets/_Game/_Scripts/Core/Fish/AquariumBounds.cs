using Game.Core.Utilities;

namespace Game.Core.Fish
{
    public sealed class AquariumBounds
    {
        public readonly float MinX, MaxX;
        public readonly float MinY, MaxY;
        public readonly float MinZ, MaxZ;

        public AquariumBounds(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            MinX = minX; MaxX = maxX;
            MinY = minY; MaxY = maxY;
            MinZ = minZ; MaxZ = maxZ;
        }

        public bool IsInside(Vec3 pos)
        {
            return
                pos.X >= MinX && pos.X <= MaxX &&
                pos.Y >= MinY && pos.Y <= MaxY &&
                pos.Z >= MinZ && pos.Z <= MaxZ;
        }
    }
}
