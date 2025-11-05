using Game.Core.Extensions;
using Game.Core.Light;
using Game.Core.Trigger;
using Sirenix.Serialization;
using UnityEngine;
using VContainer;

namespace Game.Domains.Trigger
{
    public sealed class SwitchLight_TriggerAction : BaseTriggerAction
    {
        [SerializeField] private bool _turnOn;
        private ILightBehaviour _lightBehaviour;

        public override void Inject(GameObject triggerGO, IObjectResolver objectResolver)
        {
            _lightBehaviour = triggerGO.GetComponentInChildren<ILightBehaviour>();
            base.Inject(triggerGO, objectResolver);
        }

        protected override void OnTriggered()
        {
            if (_turnOn)
                _lightBehaviour.TurnOn();
            else
                _lightBehaviour.TurnOff();
        }
    }
}
