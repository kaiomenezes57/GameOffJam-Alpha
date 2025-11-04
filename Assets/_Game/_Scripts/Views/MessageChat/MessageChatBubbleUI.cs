using Game.Core.MessageChat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.MessageChat
{
    public sealed class MessageChatBubbleUI : MonoBehaviour, IMessageChatBubbleUI
    {
        [SerializeField] private HorizontalLayoutGroup _layoutGroup;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _backgroundImage;

        public void Setup(string message, MessageChatSenderType sender)
        {
            var anchorSide = sender == MessageChatSenderType.Player ?
                TextAnchor.MiddleRight :
                TextAnchor.MiddleLeft;

            var backgroundColor = sender == MessageChatSenderType.Player ?
                Color.blue :
                Color.gray;

            _text.text = message;
            _layoutGroup.childAlignment = anchorSide;
            _backgroundImage.color = backgroundColor;
        }

        public void Clear()
        {
            _text.text = string.Empty;
        }
    }
}