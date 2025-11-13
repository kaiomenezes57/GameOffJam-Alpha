using UnityEngine;
using Game.Core.Trigger;
using Sirenix.Serialization;
using VContainer;
using Cysharp.Threading.Tasks;
using System.Linq;
using Sirenix.OdinInspector;

namespace Game.Domains.Trigger
{
    public abstract class BaseGameTrigger : MonoBehaviour, IGameTrigger
    {
        [OdinSerialize, SerializeReference] private ITriggerAction[] _actions;
        [Inject] private readonly IObjectResolver _objectResolver;

        private void Start()
        {
            if (_objectResolver == null)
            {
#if DEBUG
                Debug.LogError("[Base Game Trigger] IObjectResolver could not be injected before Start().");
#endif
                return;
            }

            foreach (var action in _actions)
                action.Inject(gameObject, _objectResolver);
        }

        [Button]
        public async void TriggerActions()
        {
            var actionsList = _actions.ToList();
            if (await UniTask.WaitUntil(() => actionsList.TrueForAll(a => a.Ready),
                cancellationToken: destroyCancellationToken)
                .SuppressCancellationThrow())
                return;
            
            foreach (var action in _actions)
                action.Trigger(destroyCancellationToken);
        }
    }
}