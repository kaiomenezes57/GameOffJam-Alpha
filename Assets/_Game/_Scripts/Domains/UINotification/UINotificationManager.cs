using System.Collections.Generic;
using Game.Core.UINotification;
using VContainer;

namespace Game.Domains.UINotification
{
    public sealed class UINotificationManager : IUINotificationManager
    {
        [Inject] private readonly IUINotificationView _notificationView;
        private readonly Queue<UINotificationDataSO> _notificationQueue = new();

        public void Show(UINotificationDataSO notificationData)
        {
            _notificationQueue.Enqueue(notificationData);
            if (_notificationQueue.Count == 1)
                ShowNextNotification();
        }

        private void ShowNextNotification()
        {
            if (_notificationQueue.Count == 0)
                return;

            var nextNotification = _notificationQueue.Dequeue();
            if (!nextNotification.IsValid())
                return;

            _notificationView.Display(
                nextNotification.Message.GetLocalizedString(),
                nextNotification.Icon,
                onHide: ShowNextNotification);
        }
    }
}