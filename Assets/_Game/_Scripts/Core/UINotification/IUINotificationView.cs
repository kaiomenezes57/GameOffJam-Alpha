using System;
using UnityEngine;

namespace Game.Core.UINotification
{
    public interface IUINotificationView
    {
        void Display(string message, Sprite icon, Action onHide);
    }
}
