using Game.Core.Interaction;
using Game.Core.Light;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Views
{
    public sealed class LightSwitcher : BaseInteractable
    {
        [SerializeField] private GameObject _lightObj;
        private ILightBehaviour _lightBehaviour;

        private void Start()
        {
            _lightBehaviour = _lightObj.GetComponent<ILightBehaviour>();
        }

        protected override void OnInteract()
        {
            _lightBehaviour?.Switch(0f);
        }

        public override bool CanInteract()
        {
            return true;
        }
    }
}
