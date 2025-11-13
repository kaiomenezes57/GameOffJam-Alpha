using DG.Tweening;
using Game.Core.Events;
using Game.Core.Interaction;
using UnityEngine;

namespace Game.Views.Interaction
{
    [RequireComponent(typeof(OutlineFx.OutlineFx))]
    public sealed class InteractionOutline : MonoBehaviour
    {
        private OutlineFx.OutlineFx _outline;
        private IInteractable _owner;

        private void Awake()
        {
            _outline = GetComponent<OutlineFx.OutlineFx>();

            _outline.Color = Color.green;
            _outline.Alpha = 0f;

            _owner = GetComponent<IInteractable>();
            _owner ??= GetComponentInParent<IInteractable>();
            _owner ??= GetComponentInChildren<IInteractable>();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnUpdateInteraction>(Switch);
        }

        private void OnDisable()
        {
            EventBus.UnSubscribe<OnUpdateInteraction>(Switch);
        }

        private void Switch(OnUpdateInteraction interaction)
        {
            var initialAlpha = _outline.Alpha;
            var targetAlpha = interaction != null && 
                interaction.Interactable == _owner ? 1f : 0f;

            DOVirtual.Float(initialAlpha, targetAlpha, 0.1f, value => _outline.Alpha = value);
        }
    }
}
