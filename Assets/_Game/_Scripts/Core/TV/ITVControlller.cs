using FMODUnity;
using Game.Core.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Video;

namespace Game.Core.TV
{
    public interface ITVControlller
    {
        TVState CurrentState { get; }
        void StartContent(TVContent content);
        void Hack();
    }

    [System.Serializable]
    public struct TVContent : IValidator
    {
        [field: Title("Settings")]
        [field: SerializeField] public LocalizedString ContentName { get; private set; }
        [field: SerializeField] public VideoClip Clip { get; private set; }
        [field: SerializeField] public EventReference Audio { get; private set; }

        [field: Title("Unity Events")]
        [field: SerializeField] public UnityEvent OnStartContent { get; private set; }
        [field: SerializeField] public UnityEvent OnEndContent { get; private set; }

        public readonly bool IsValid()
        {
            return Clip != null;
        }
    }

    public enum TVState
    {
        Idle,
        PlayingContent,
        Hacked,
    }
}
