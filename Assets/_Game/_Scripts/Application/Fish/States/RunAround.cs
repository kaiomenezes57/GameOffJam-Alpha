using Game.Core.Fish;
using Game.Core.StateMachine;
using Game.Core.Utilities;
using System;

namespace Game.Application.Fish.States
{
    public sealed class RunAround : BaseState
    {
        private readonly FishModel _fish;
        private readonly AquariumBounds _bounds;

        private readonly Random _random = new();
        private float _nextDirectionChange;

        public RunAround(FishModel fish, AquariumBounds bounds)
        {
            _fish = fish;
            _bounds = bounds;
            
            _fish.Speed = 0.5f;
            _nextDirectionChange = RandomTime();
        }

        public override void Enter(IStateMachine stateMachine)
        {
            NextState = new WanderAround(_fish, _bounds);
        }

        public override void Tick(IStateMachine stateMachine, float dt)
        {
            _nextDirectionChange -= dt;

            if (_nextDirectionChange <= 0)
            {
                _fish.Direction = RandomFlatDirection();
                _nextDirectionChange = RandomTime();
            }
            
            _fish.Position += _fish.Direction * _fish.Speed * dt;

            if (!_bounds.IsInside(_fish.Position))
                ReflectDirection();
        }

        private float RandomTime() => (float)(_random.NextDouble() * 2.5 + 0.5); // 0.5–3s

        private Vec3 RandomFlatDirection()
        {
            double angle = _random.NextDouble() * Math.PI * 2;
            return new Vec3((float)Math.Cos(angle), 0, (float)Math.Sin(angle));
        }

        private void ReflectDirection()
        {
            var d = _fish.Direction;
            float x = d.X, y = d.Y, z = d.Z;

            if (_fish.Position.X < _bounds.MinX || _fish.Position.X > _bounds.MaxX)
                x = -x;

            if (_fish.Position.Z < _bounds.MinZ || _fish.Position.Z > _bounds.MaxZ)
                z = -z;

            _fish.Direction = new Vec3(x, y, z).Normalized();
        }
    }
}