using FMODUnity;
using System;

namespace Game.Core.Dialogue
{
    public interface IDialogueAudioService
    {
        event Action OnLineEnd;
        void Play(EventReference audioEvent);
        void Stop();
    }
}
