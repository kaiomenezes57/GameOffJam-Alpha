using Game.Application.Fish.States;
using Game.Core.Fish;
using Game.Core.StateMachine;

namespace Game.Application.Fish
{
    public sealed class FishStateMachine : BaseStateMachine
    {
        protected override IState InitialState => new WanderAround(_model, _bounds);

        private readonly FishModel _model;
        private readonly AquariumBounds _bounds;

        public FishStateMachine(FishModel model, AquariumBounds bounds)
        {
            _model = model;
            _bounds = bounds;
        }
    }
}