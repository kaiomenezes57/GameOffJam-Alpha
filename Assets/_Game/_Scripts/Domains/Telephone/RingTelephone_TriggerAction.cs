using Game.Core.Dialogue;
using Game.Core.Telephone;
using Game.Core.Trigger;
using Game.Core.UINotification;
using UnityEngine;
using VContainer;

namespace Game.Domains.Telephone
{
    public sealed class RingTelephone_TriggerAction : BaseTriggerAction
    {
        [SerializeField] private DialogueData _dialogueData;
        private IUINotificationManager _notificationManager;
        private ITelephone _telephone;

        public override void Inject(GameObject triggerGO, IObjectResolver objectResolver)
        {
            _notificationManager = objectResolver.Resolve<IUINotificationManager>();
            _telephone = objectResolver.Resolve<ITelephone>();
            base.Inject(triggerGO, objectResolver);
        }

        protected override void OnTriggered()
        {
            _notificationManager.Show(Resources.Load<UINotificationDataSO>("IncomingPhoneCall [NOTIFICATION DATA]"));
            _telephone.Ring(_dialogueData);
        }
    }
}