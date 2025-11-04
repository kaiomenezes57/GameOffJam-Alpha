using Game.Core.MessageChat;

namespace Game.Core.Events
{
    public sealed class OnSendMessageOnChat : IGameEvent
    {
        public MessageChatData MessageChatData { get; }
        
        public OnSendMessageOnChat(MessageChatData messageChatData)
        {
            MessageChatData = messageChatData;
        }

        public bool IsValid()
        {
            return MessageChatData.IsValid();
        }
    }
}
