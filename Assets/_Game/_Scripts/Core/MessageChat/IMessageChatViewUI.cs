using System;

namespace Game.Core.MessageChat
{
    public interface IMessageChatViewUI
    {
        event Action OnRequestNextMessage;
        void ShowMessage(MessageChatData data);
        void OnEndChat();
    }
}
