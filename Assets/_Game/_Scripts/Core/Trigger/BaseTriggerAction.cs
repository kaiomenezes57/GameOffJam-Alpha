using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;
using VContainer;

namespace Game.Core.Trigger
{
    public abstract class BaseTriggerAction : ITriggerAction
    {
        [Title("Base Settings")]
        [SerializeField] private bool _triggerOnce = true;
        [SerializeField, Min(0f)] private float _delay;
        
        public bool Ready { get; private set; }
        private bool _triggered;

        [Title("Unity Events")]
        [SerializeField] private UnityEvent _onStartTriggering;
        [SerializeField] private UnityEvent _onTrigger;

        public virtual void Inject(GameObject triggerGO, IObjectResolver objectResolver)
        {
            Ready = true;
        }

        public async void Trigger(CancellationToken token)
        {
            if (_triggerOnce && _triggered)
                return;

            _onStartTriggering?.Invoke();
            _triggered = true;

            if (await UniTask.WaitForSeconds(_delay, cancellationToken: token)
                .SuppressCancellationThrow())
                return;

            _onTrigger?.Invoke();
            OnTriggered();
        }

        protected abstract void OnTriggered();
    }
}
