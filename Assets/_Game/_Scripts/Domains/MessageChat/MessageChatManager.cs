using Game.Core.Extensions;
using Game.Core.MessageChat;
using System;
using System.Collections.Generic;
using UnityEngine.Localization.Tables;
using VContainer;

namespace Game.Domains.MessageChat
{
    public sealed class MessageChatManager : IMessageChatManager, IDisposable
    {
        private readonly IPlayerInputChatMessageViewUI _playerInputChatMessageService;
        private readonly IMessageChatViewUI _messageChatViewUI;
        private readonly Queue<MessageChatData> _messageLinesQueue = new();

        [Inject]
        public MessageChatManager(
            IPlayerInputChatMessageViewUI playerInputChatMessageService, 
            IMessageChatViewUI messageChatViewUI)
        {
            _playerInputChatMessageService = playerInputChatMessageService;
            _messageChatViewUI = messageChatViewUI;
            _messageChatViewUI.OnRequestNextMessage += NextMessage;
        }

        public void Dispose()
        {
            _messageChatViewUI.OnRequestNextMessage -= NextMessage;
        }

        public async void ShowMessage(StringTable stringTable)
        {
            var allEntries = await stringTable.GetAllEntries();
            if (allEntries.Count == 0)
                return;

            _messageLinesQueue.Clear();

            foreach (var entry in allEntries)
            {
                var message = entry[(entry.IndexOf(">") + 1)..].Trim();
                var delay = float.Parse(entry.GetSubstringBetween("<", ">").Trim());
                var sender = (MessageChatSenderType)int.Parse(entry.GetSubstringBetween("[", "]").Trim());

                var messageChatData = new MessageChatData(message, delay, sender);
                if (!messageChatData.IsValid())
                    continue;
                
                _messageLinesQueue.Enqueue(messageChatData);
            }

            NextMessage();
        }

        private void NextMessage()
        {
            if (_messageLinesQueue.Count == 0)
            {
                EndChat();
                return;
            }
            
            var messageData = _messageLinesQueue.Dequeue();
            
            if (messageData.Sender == MessageChatSenderType.Player)
            {
                _playerInputChatMessageService.StartTyping(
                    messageData.Message, 
                    () => _messageChatViewUI.ShowMessage(messageData));
                return;
            }

            _messageChatViewUI.ShowMessage(messageData);
        }

        private void EndChat()
        {
            _messageLinesQueue.Clear();
            _messageChatViewUI.OnEndChat();
        }
    }
}
