using Game.Core.Trigger;
using Game.Core.UINotification;
using UnityEngine;
using VContainer;

namespace Game.Domains.UINotification
{
    public sealed class ShowUINotification_TriggerAction : BaseTriggerAction
    {
        [SerializeField] private UINotificationDataSO _notificationData;
        private IUINotificationManager _notificationManager;

        public override void Inject(GameObject triggerGO, IObjectResolver objectResolver)
        {
            _notificationManager = objectResolver.Resolve<IUINotificationManager>();
            base.Inject(triggerGO, objectResolver);
        }

        protected override void OnTriggered()
        {
            _notificationManager?.Show(_notificationData);
        }
    }
}