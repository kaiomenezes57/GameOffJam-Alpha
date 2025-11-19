using Game.Core.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Unity.Crosshair
{
    public sealed class CrosshairInteractionResponder : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _interactionText;
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
            _interactionText.text = string.Empty;
        }

        private void Switch(OnUpdateInteraction data)
        {
            bool isValid = data.Interactable != null && data.Interactable.CanInteract();
            
            _image.sprite = isValid ?
                 _onSprite :
                 _offSprite;

            _interactionText.text = isValid && data.InformationProvider != null ?
                data.InformationProvider.GetInteractionText() :
                string.Empty;
        }
    }
}