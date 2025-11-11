using DG.Tweening;
using Game.Core.Interaction;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Domains.Door
{
    public sealed class DoorInteraction : BaseInteractable
    {
        [Title("References")]
        [SerializeField, Required] private GameObject _door;
        [SerializeField, Required] private Transform _openedTransform;
        [SerializeField, Required] private Transform _closedTransform;

        [Title("Settings")]
        [SerializeField] private bool _startsOpened;
        [SerializeField] private float _animationDuration = 0.5f;
        private bool _isOpened;

        [Title("Unity Events")]
        [SerializeField] private UnityEvent _onOpened;
        [SerializeField] private UnityEvent _onClosed;

        private void Start()
        {
            var targetTransform = _isOpened ?
                _openedTransform :
                _closedTransform;
            _door.transform.rotation = targetTransform.rotation;
            _isOpened = _startsOpened;
        }

        protected override void OnInteract()
        {
            var targetTransform = _isOpened ? 
                _closedTransform : 
                _openedTransform;

            _isOpened = !_isOpened;

            _door.transform.DORotate(targetTransform.eulerAngles, _animationDuration)
                .OnComplete(() => {
                    var unityEvent = _isOpened ?
                        _onOpened :
                        _onClosed;
                    unityEvent?.Invoke();
                });
        }

        public override bool CanInteract()
        {
            return true;
        }
    }
}
