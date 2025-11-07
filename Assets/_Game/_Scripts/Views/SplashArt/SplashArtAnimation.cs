using DG.Tweening;
using Game.Core.Scene;
using Game.Core.Scene.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using VContainer;

namespace Game.Views.SplashArt
{
    public sealed class SplashArtAnimation : MonoBehaviour
    {
        [SerializeField] private SceneDataSO _nextScene;
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private UnityEvent _onVideoStarted;
        [Inject] private readonly ISceneController _sceneController;

        private void Start()
        {
            _videoPlayer.loopPointReached += OnVideoEnd;
            DOVirtual.DelayedCall(1f, () => {
                _videoPlayer.Play();
                _onVideoStarted?.Invoke();
            });
        }

        private void OnVideoEnd(VideoPlayer source)
        {
            _videoPlayer.loopPointReached -= OnVideoEnd;
            _sceneController.LoadScene(_nextScene);
        }
    }
}
