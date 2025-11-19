using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Application.Showcase
{
    public sealed class ShowcaseCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _camera;

        private void Start()
        {
            _camera.enabled = false;
        }

        public void StartAnimation()
        {
            _camera.enabled = true;
        }

        public void StopAnimation()
        {
            _camera.enabled = false;
        }

        private void OnDisable()
        {
            StopAnimation();
        }
    }
}
