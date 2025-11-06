using Game.Core.Utilities;
using UnityEngine;
using UnityEngine.Localization;

namespace Game.Core.UINotification
{
    [CreateAssetMenu(menuName = "Assets/Notification Data")]
    public sealed class UINotificationDataSO : ScriptableObject, IValidator
    {
        [field: SerializeField] public LocalizedString Message { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }

        public bool IsValid()
        {
            return Message != null;
        }
    }
}
