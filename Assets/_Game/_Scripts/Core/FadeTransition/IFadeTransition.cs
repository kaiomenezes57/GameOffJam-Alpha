using System;

namespace Game.Core.FadeTransition
{
    public interface IFadeTransition
    {
        void Perform(float blackScreenDuration,
            float fadeDuration = 0.5f,
            Action onFadeInStarted = null,
            Action onFadeInCompleted = null,
            Action onFadeOutCompleted = null);
    }
}