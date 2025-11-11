using Game.Core.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.Crosshair
{
    public sealed class CrosshairInteractionResponder : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        private void OnEnable()
        {
            EventBus.Subscribe<OnUpdateInteraction>(Switch);
        }

        private void OnDisable()
        {
            EventBus.UnSubscribe<OnUpdateInteraction>(Switch);
        }

        private void Start()
        {
            _image.sprite = _offSprite;
        }

        private void Switch(OnUpdateInteraction data)
        {
            _image.sprite = data.Interactable != null && data.Interactable.CanInteract() ?
                 _onSprite :
                 _offSprite;
        }
    }
}