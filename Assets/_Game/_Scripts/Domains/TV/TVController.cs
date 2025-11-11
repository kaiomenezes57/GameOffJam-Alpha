using Game.Core.TV;
using UnityEngine;
using UnityEngine.Video;

namespace Game.Domains.TV
{
    public sealed class TVController : MonoBehaviour, ITVControlller
    {
        [SerializeField] private VideoPlayer _videoPlayer;

        public TVState CurrentState => _currentState;
        private TVState _currentState;

        public void StartContent(TVContent content)
        {
            if (!content.IsValid())
            {
                Debug.LogWarning("Invalid TV Content");
                return;
            }

            _videoPlayer.clip = content.Clip;
            _videoPlayer.Play();

            content.OnStartContent?.Invoke();
            _currentState = TVState.PlayingContent;
        }

        public void Hack()
        {
            _currentState = TVState.Hacked;
        }
    }
}
