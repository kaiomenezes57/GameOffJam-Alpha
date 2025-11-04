using System;

namespace Game.Core.MessageChat
{
    public interface IPlayerInputChatMessageViewUI
    {
        void StartTyping(string message, Action onComplete);
    }
}
