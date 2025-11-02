using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Views.Buttons
{
    public sealed class ButtonBehaviour : MonoBehaviour
    {
        [OdinSerialize, SerializeReference] private IButtonAction[] _actions;
        [SerializeField, MinValue(0f)] private float _clickCooldown = 1f;
        [SerializeField] private Button _button;
        private bool _isInCooldown;

        private void OnEnable()
        {
            foreach (var action in _actions)
                _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            foreach (var action in _actions)
                _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (_isInCooldown) return;
            _isInCooldown = true;

            foreach (var action in _actions)
                action.OnClick();

            DOVirtual.DelayedCall(_clickCooldown, () => _isInCooldown = false);
            EventSystem.current?.SetSelectedGameObject(null);
        }
    }
}
