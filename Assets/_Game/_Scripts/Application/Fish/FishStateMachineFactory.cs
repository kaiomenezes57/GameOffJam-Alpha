using Game.Core.Fish;
using Game.Core.StateMachine;

namespace Game.Application.Fish
{
    public sealed class FishStateMachineFactory : IFishStateMachineFactory
    {
        public IStateMachine Create(FishModel fishModel, AquariumBounds aquariumBounds)
        {
            return new FishStateMachine(fishModel, aquariumBounds);
        }
    }
}