using Game.Core.MessageChat;

namespace Game.Unity.MessageChat
{
    public interface IMessageChatBubbleUI
    {
        void Setup(string message, MessageChatSenderType sender);
        void Clear();
    }
}