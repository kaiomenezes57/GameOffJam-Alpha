using UnityEngine.Localization.Tables;
using Game.Core.UINotification;
using Game.Core.MessageChat;
using Game.Core.Smartphone;
using Game.Core.Trigger;
using UnityEngine;
using VContainer;

namespace Game.Domains.MessageChat
{
    public sealed class StartMessageChat_TriggerAction : BaseTriggerAction
    {
        [SerializeField] private StringTable _messageChatData;
        private IUINotificationManager _notificationManager;
        private IMessageChatManager _messageChatManager;
        private IPhoneManager _phoneManager;

        public override void Inject(GameObject triggerGO, IObjectResolver objectResolver)
        {
            _notificationManager = objectResolver.Resolve<IUINotificationManager>();
            _messageChatManager = objectResolver.Resolve<IMessageChatManager>();
            _phoneManager = objectResolver.Resolve<IPhoneManager>();

            base.Inject(triggerGO, objectResolver);
        }

        protected override void OnTriggered()
        {
            if (Resources.Load<UINotificationDataSO>("NewPhoneMessage [NOTIFICATION DATA]") 
                is { } notificationData)
            {
                _notificationManager.Show(notificationData);
                _phoneManager.SetNextMode(PhoneScreenType.Message);
                _messageChatManager.ShowMessage(_messageChatData);

                return;
            }

#if DEBUG
            Debug.LogError("UINotificationDataSO for New Phone Message not found in Resources folder.");
#endif
        }
    }
}
