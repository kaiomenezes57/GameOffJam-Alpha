using Game.Core.Utilities;

namespace Game.Core.Fish
{
    public sealed class FishModel
    {
        public Vec3 Position;
        public Vec3 Direction;
        public float Speed;

        public FishModel(Vec3 startPosition)
        {
            Position = startPosition;
            Direction = new Vec3(1, 0, 0);
        }
    }
}
