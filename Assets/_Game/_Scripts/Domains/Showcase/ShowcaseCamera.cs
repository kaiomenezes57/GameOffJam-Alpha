using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Domains.Showcase
{
    public sealed class ShowcaseCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineSplineDolly _dolly;
        [SerializeField] private float _loopDuration;
        private Sequence _animationSequence;

        private void Start()
        {
            _dolly.VirtualCamera.enabled = false;
        }

        public void StartAnimation()
        {
            _dolly.VirtualCamera.enabled = true;
            _animationSequence?.Kill();
            _animationSequence = DOTween.Sequence()
                .Append(GetAnimationStep(1f))
                .Append(GetAnimationStep(0f))
                .SetLoops(-1, LoopType.Restart)
                .SetLink(gameObject);
        }

        public void StopAnimation()
        {
            _dolly.VirtualCamera.enabled = false;
            _animationSequence?.Kill();
        }

        private Tween GetAnimationStep(float to)
        {
            return DOVirtual.Float(_dolly.CameraPosition, 
                to, 
                _loopDuration, 
                (value) => _dolly.CameraPosition = value);
        }

        private void OnDisable()
        {
            StopAnimation();
        }
    }
}
