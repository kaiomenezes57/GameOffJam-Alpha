using Game.Core.PhoneNotepad;
using VContainer;
using Game.Core.Trigger;
using UnityEngine;
using Game.Core.UINotification;

namespace Game.Domains.PhoneNotepad
{
    public sealed class NewPhoneNotepadTask_TriggerAction : BaseTriggerAction
    {
        [SerializeField] private PhoneNotepadTaskData[] _tasks;
        private IUINotificationManager _notificationManager;
        private IPhoneNotepadManager _phoneNotepadManager;

        public override void Inject(GameObject triggerGO, IObjectResolver objectResolver)
        {
            _notificationManager = objectResolver.Resolve<IUINotificationManager>();
            _phoneNotepadManager = objectResolver.Resolve<IPhoneNotepadManager>();
            base.Inject(triggerGO, objectResolver);
        }

        protected override void OnTriggered()
        {
            _notificationManager.Show(Resources.Load<UINotificationDataSO>("NewTask [NOTIFICATION DATA]"));
            _phoneNotepadManager.SetTasks(_tasks);
        }
    }
}
