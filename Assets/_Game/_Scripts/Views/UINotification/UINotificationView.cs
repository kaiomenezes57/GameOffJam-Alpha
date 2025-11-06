using DG.Tweening;
using Game.Core.UINotification;
using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Views.UINotification
{
    public sealed class UINotificationView : MonoBehaviour, IUINotificationView
    {
        [Title("References")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _message;
        [SerializeField] private Sprite _defaultIcon;
        [SerializeField] private Image _icon;

        [Title("Unity Events")]
        [SerializeField] private UnityEvent _onShow;
        [SerializeField] private UnityEvent _onHide;
        private Sequence _shownSequence;

        private void Start()
        {
            _shownSequence?.Kill();
            _shownSequence = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(0.5f, duration: 0.5f))
                .Append(_canvasGroup.DOFade(1.0f, duration: 0.5f))
                .SetLoops(10, LoopType.Restart)
                .OnComplete(Hide)
                .SetLink(gameObject)
                .Pause();
            
            _canvasGroup.alpha = 0f;
            _message.text = string.Empty;
            _icon.sprite = null;
        }

        private void OnDisable()
        {
            _shownSequence?.Kill();
        }

        public void Display(string message, Sprite icon, Action onHide)
        {
            _onHide.AddListener(() => onHide?.Invoke());

            _canvasGroup.DOKill();
            _canvasGroup.alpha = 0f;

            _message.text = message;
            _icon.sprite = icon == null ? 
                _defaultIcon : 
                icon;
            
            _onShow?.Invoke();

            _canvasGroup.DOFade(1f, duration: 1f)
                .OnComplete(() => {
                    _shownSequence.Restart();
                    _shownSequence.Play();
                })
                .SetLink(gameObject);
        }

        private void Hide()
        {
            _shownSequence.Pause();

            _canvasGroup.DOFade(0f, duration: 1f)
                .SetLink(gameObject);

            _onHide?.Invoke();
            _onHide.RemoveAllListeners();
        }
    }
}
