using Cysharp.Threading.Tasks;
using Game.Core.Events;
using Game.Core.MessageChat;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Game.Views.MessageChat
{
    public sealed class MessageChatViewUI : MonoBehaviour, IMessageChatViewUI
    {
        [SerializeField] private MessageChatBubbleUI _messageBubblePrefab;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Transform _container;

        [SerializeField] private TextMeshProUGUI _contactStatusText;
        [SerializeField] private LocalizedString _typingStatus;
        [SerializeField] private UnityEvent _onEndChat;

        private IObjectPool<IMessageChatBubbleUI> _messageChatBubblePool;
        public event Action OnRequestNextMessage;

        private void Awake()
        {
            _messageChatBubblePool = new ObjectPool<IMessageChatBubbleUI>(
                createFunc: () => {
                    var messageBubble = Instantiate(_messageBubblePrefab.gameObject, _container);
                    return messageBubble.GetComponent<IMessageChatBubbleUI>();
                },
                actionOnGet: (bubble) => {
                    if (bubble is not MonoBehaviour mono) return;
                    mono.gameObject.SetActive(true);
                },
                actionOnRelease: (bubble) => {
                    if (bubble is not MonoBehaviour mono) return;
                    mono.gameObject.SetActive(false);
                    bubble.Clear();
                },
                actionOnDestroy: (bubble) => {
                    if (bubble is not MonoBehaviour mono) return;
                    bubble.Clear();

                    Destroy(mono.gameObject);
                });

            _contactStatusText.text = string.Empty;
        }

        private void OnDisable()
        {
            _messageChatBubblePool.Clear();
            _contactStatusText.text = string.Empty;
        }

        public async void ShowMessage(MessageChatData data)
        {
            _contactStatusText.text = _typingStatus.GetLocalizedString();

            if (await UniTask.WaitForSeconds(data.Delay, cancellationToken: destroyCancellationToken)
                .SuppressCancellationThrow())
                return;

            var messageChatBubble = _messageChatBubblePool.Get();
            messageChatBubble?.Setup(data.Message, data.Sender);
            _contactStatusText.text = string.Empty;

            _scrollRect.verticalNormalizedPosition = 0f;

            EventBus.Raise(new OnSendMessageOnChat(data));
            OnRequestNextMessage?.Invoke();
        }

        public void OnEndChat()
        {
            _onEndChat?.Invoke();
        }
    }
}