using Game.Core.MessageChat;
using Game.Core.Trigger;
using UnityEngine;
using UnityEngine.Localization.Tables;
using VContainer;

namespace Game.Domains.Trigger
{
    public sealed class StartMessageChat_TriggerAction : BaseTriggerAction
    {
        [SerializeField] private StringTable _messageChatData;
        private IMessageChatManager _messageChatManager;

        public override void Inject(IObjectResolver objectResolver)
        {
            _messageChatManager = objectResolver.Resolve<IMessageChatManager>();
            base.Inject(objectResolver);
        }
        protected override void OnTriggered()
        {
            _messageChatManager.ShowMessage(_messageChatData);
        }
    }
}
