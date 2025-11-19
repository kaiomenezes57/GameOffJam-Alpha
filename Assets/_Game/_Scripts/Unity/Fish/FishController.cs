using Game.Core.Extensions;
using Game.Core.Fish;
using Game.Core.StateMachine;
using Game.Core.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Game.Unity.Fish
{
    public sealed class FishController : MonoBehaviour
    {
        [Inject] private readonly IFishStateMachineFactory _fishStateMachineFactory;
        [SerializeField] private BoxCollider _areaBounds;

        private FishModel _fishModel;
        private IStateMachine _stateMachine;

        private void Start()
        {
            var initialPosition = transform.position.ToApp();
            _fishModel = new FishModel(initialPosition);
            
            var aquariumBounds = new AquariumBounds(
                _areaBounds.bounds.min.x,
                _areaBounds.bounds.max.x,
                _areaBounds.bounds.min.y,
                _areaBounds.bounds.max.y,
                _areaBounds.bounds.min.z,
                _areaBounds.bounds.max.z);
            
            _stateMachine = _fishStateMachineFactory.Create(_fishModel, aquariumBounds);
            (_stateMachine as IStateMachineInitializer)?.Initialize();
        }

        private void Update()
        {
            if (_stateMachine is not IStateMachineTicker tick)
                return;
            
            tick.Tick(Time.deltaTime);
            
            transform.position = _fishModel.Position.ToUnity();
            
            var forward = new Vector3(_fishModel.Direction.X, 0, _fishModel.Direction.Z);
            if (forward != Vector3.zero)
                transform.forward = Vector3.Lerp(transform.forward, forward, Time.deltaTime * 5f);
        }

        [Button]
        public void ChangeToNextState()
        {
            var nextState = _stateMachine.Current.NextState;
            _stateMachine.ChangeState(nextState);
        }
    }
}
