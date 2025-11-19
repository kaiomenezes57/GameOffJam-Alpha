using DG.Tweening;
using Game.Core.Extensions;
using Game.Core.Showcase;
using System;
using TMPro;
using UnityEngine;

namespace Game.Unity.Showcase
{
    public sealed class ShowcaseText : MonoBehaviour, IShowcaseShow, IShowcaseHide, IShowcaseEvents
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _header;
        [SerializeField] private TextMeshProUGUI _message;

        private Vector3 _activatedHeaderPosition;
        private Vector3 _activatedMessagePosition;

        private Vector3 _desactivatedHeaderPosition;
        private Vector3 _desactivatedMessagePosition;


        public event Action OnClose;

        private void Start()
        {
            _activatedHeaderPosition = _header.rectTransform.anchoredPosition;
            _activatedMessagePosition = _message.rectTransform.anchoredPosition;

            _desactivatedHeaderPosition = _activatedHeaderPosition.WithOffset(x: -1000f);
            _desactivatedMessagePosition = _activatedMessagePosition.WithOffset(y: 1000f);

            Hide();
        }

        public void Show(string header, string message)
        {
            _header.text = header;
            _message.text = message;

            _header.rectTransform.anchoredPosition = _desactivatedHeaderPosition;
            _message.rectTransform.anchoredPosition = _desactivatedMessagePosition;

            _canvasGroup.blocksRaycasts = true;

            _canvasGroup.DOFade(1f, 0.5f).SetLink(gameObject);
            _header.rectTransform.DOAnchorPos(_activatedHeaderPosition, 0.5f)
                .SetEase(Ease.OutCubic)
                .SetLink(gameObject);
            _message.rectTransform.DOAnchorPos(_activatedMessagePosition, 0.5f)
                .SetEase(Ease.OutCubic)
                .SetLink(gameObject);
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0f;
            _header.text = string.Empty;
            _message.text = string.Empty;

            _canvasGroup.blocksRaycasts = false;

            _header.rectTransform.anchoredPosition = _desactivatedHeaderPosition;
            _message.rectTransform.anchoredPosition = _desactivatedMessagePosition;

            OnClose?.Invoke();
        }
    }
}
