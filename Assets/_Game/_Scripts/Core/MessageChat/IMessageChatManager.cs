using UnityEngine.Events;
using UnityEngine.Localization.Tables;

namespace Game.Core.MessageChat
{
    public interface IMessageChatManager
    {
        void ShowMessage(StringTable stringTable, UnityEvent onChatEnd);
    }
}
