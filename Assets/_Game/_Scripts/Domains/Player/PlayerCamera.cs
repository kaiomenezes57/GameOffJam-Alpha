using Game.Core.Utilities.DisablableComponent;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using UnityEngine;
using Game.Core.GameUserConfig;

namespace Game.Domains.Player
{
    public sealed class PlayerCamera : BaseDisablableComponent
    {
        [SerializeField] private InputActionReference _lookAxis;
        [SerializeField] private GameUserConfigDataSO _userConfig;
        [SerializeField] private CinemachineCamera _camera;
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
            if (!Enabled)
                return;

            var lookInput = _lookAxis.action.ReadValue<Vector2>();
            var yaw = lookInput.x * _userConfig.MouseSensitivity;

            transform.Rotate(Vector3.up * yaw);

            _cameraPitch -= lookInput.y * _userConfig.MouseSensitivity;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -75f, 75f);

            _camera.transform.localRotation = Quaternion.Euler(
                _cameraPitch,
                _camera.transform.localEulerAngles.y,
                _camera.transform.localEulerAngles.z);
        }
    }
}
