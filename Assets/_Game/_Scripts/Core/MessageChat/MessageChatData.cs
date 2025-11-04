using Game.Core.Utilities;
using UnityEngine;

namespace Game.Core.MessageChat
{
    public struct MessageChatData : IValidator
    {
        [field: SerializeField] public string Message { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public MessageChatSenderType Sender { get; private set; }

        public MessageChatData(string message, float delay, MessageChatSenderType sender)
        {
            Message = message;
            Delay = delay;
            Sender = sender;
        }


        public readonly bool IsValid()
        {
            return !string.IsNullOrEmpty(Message) &&
                Delay >= 0f;
        }
    }
}