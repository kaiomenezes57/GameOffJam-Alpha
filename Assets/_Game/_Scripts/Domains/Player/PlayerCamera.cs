using UnityEngine.InputSystem;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Domains.Player
{
    public sealed class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private InputActionReference _lookAxis;
        [SerializeField] private CinemachineCamera _camera;
        private const float SENSITIVITY = 0.1f;
        private float _cameraPitch = 0.0f;

        private void OnEnable()
        {
            _lookAxis.action.Enable();
        }

        private void OnDisable()
        {
            _lookAxis.action.Disable();
        }

        private void LateUpdate()
        {
            var lookInput = _lookAxis.action.ReadValue<Vector2>();
            var yaw = lookInput.x * SENSITIVITY;

            transform.Rotate(Vector3.up * yaw);

            _cameraPitch -= lookInput.y * SENSITIVITY;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -75f, 75f);

            _camera.transform.localRotation = Quaternion.Euler(
                _cameraPitch,
                _camera.transform.localEulerAngles.y,
                _camera.transform.localEulerAngles.z);
        }
    }
}
