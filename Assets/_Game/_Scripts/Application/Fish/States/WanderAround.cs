using Game.Core.Fish;
using Game.Core.StateMachine;
using Game.Core.Utilities;
using System;


namespace Game.Application.Fish.States
{
    public sealed class WanderAround : BaseState
    {
        private readonly FishModel _fish;
        private readonly AquariumBounds _bounds;
        private readonly Random _random = new();

        public WanderAround(FishModel fish, AquariumBounds bounds)
        {
            _fish = fish;
            _bounds = bounds;
            _fish.Speed = 0.1f;
        }

        public override void Enter(IStateMachine stateMachine)
        {
            NextState = new RunAround(_fish, _bounds);
        }

        public override void Tick(IStateMachine machine, float dt)
        {
            _fish.Direction = new Vec3(
                _fish.Direction.X + (float)(_random.NextDouble() - 0.5) * 0.1f,
                _fish.Direction.Y + (float)(_random.NextDouble() - 0.5) * 0.05f,
                _fish.Direction.Z + (float)(_random.NextDouble() - 0.5) * 0.1f
            ).Normalized();

            _fish.Position += _fish.Direction * _fish.Speed * dt;
            
            if (!_bounds.IsInside(_fish.Position))
                ReflectDirection();
        }

        private void ReflectDirection()
        {
            var d = _fish.Direction;
            float x = d.X, y = d.Y, z = d.Z;

            if (_fish.Position.X < _bounds.MinX || _fish.Position.X > _bounds.MaxX)
                x = -x;

            if (_fish.Position.Y < _bounds.MinY || _fish.Position.Y > _bounds.MaxY)
                y = -y;

            if (_fish.Position.Z < _bounds.MinZ || _fish.Position.Z > _bounds.MaxZ)
                z = -z;

            _fish.Direction = new Vec3(x, y, z).Normalized();
        }
    }
}