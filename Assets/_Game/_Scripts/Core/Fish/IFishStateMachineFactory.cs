using Game.Core.StateMachine;

namespace Game.Core.Fish
{
    public interface IFishStateMachineFactory
    {
        IStateMachine Create(FishModel fishModel, AquariumBounds aquariumBounds);
    }
}
