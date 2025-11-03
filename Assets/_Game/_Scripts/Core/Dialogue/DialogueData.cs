using FMODUnity;
using Game.Core.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Tables;

namespace Game.Core.Dialogue
{
    [System.Serializable]
    public struct DialogueData : IValidator
    {
        // Dialogue format in Localization Table should be:
        // [CHARACTER NAME] Dialogue text.
        // If need to change color, use <color=color>text</color> tag.

        [field: Title("Content")]
        [field: SerializeField] public StringTable DialogueTable { get; private set; }
        [field: SerializeField] public EventReference Audio { get; private set; }
        [field: SerializeField] public Transform SpeakerTransform { get; private set; }

        [field: Title("Unity Events")]
        [field: SerializeField] public UnityEvent OnDialogueStart { get; private set; }
        [field: SerializeField] public UnityEvent OnDialogueEnd { get; private set; }

        public readonly bool IsValid()
        {
            return DialogueTable != null;
        }
    }
}
