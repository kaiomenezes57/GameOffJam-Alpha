using FMOD;
using Game.Core.Utilities;
using UnityEngine;
using UnityEngine.Localization;

namespace Game.Core.Interaction
{
    [System.Serializable]
    public struct InteractionInformation : IValidator
    {
        public readonly string CommandText => _localizedCommandText.GetLocalizedString();
        [SerializeField] private LocalizedString _localizedCommandText;

        public readonly bool IsValid()
        {
            return _localizedCommandText != null && 
                !_localizedCommandText.IsEmpty;
        }
    }
}