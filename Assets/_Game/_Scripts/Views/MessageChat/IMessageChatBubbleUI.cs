using Game.Core.MessageChat;

namespace Game.Views.MessageChat
{
    public interface IMessageChatBubbleUI
    {
        void Setup(string message, MessageChatSenderType sender);
        void Clear();
    }
}