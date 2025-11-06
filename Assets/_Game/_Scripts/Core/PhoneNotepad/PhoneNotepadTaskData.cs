using UnityEngine.Localization;
using Sirenix.Serialization;
using Game.Core.Utilities;
using UnityEngine;

namespace Game.Core.PhoneNotepad
{
    [System.Serializable]
    public struct PhoneNotepadTaskData : IValidator
    {
        [field: SerializeField] public LocalizedString Task { get; private set; }
        public bool IsCompleted { get; private set; }

        [OdinSerialize, SerializeReference] public IPhoneNotepadTaskCompleteTrigger TaskCompleteTrigger;

        public readonly bool IsValid()
        {
            //return Task != null && 
            //    TaskCompleteTrigger != null;

            return true;
        }


        public void SetCompleted() 
        {
            IsCompleted = true;
        }
    }
}
