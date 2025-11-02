using UnityEngine.InputSystem;
using UnityEngine;

namespace Game.Domains.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private InputActionReference _moveAxis;
        [SerializeField] private InputActionReference _sprintInput;
        [SerializeField] private float _baseMovementSpeed;

        private const float GRAVITY_VALUE = -9.81f;
        private float _currentMovementSpeed;
        private Vector3 _motion = new();

        private void OnEnable()
        {
            _moveAxis.action.Enable();
            _sprintInput.action.Enable();
            _sprintInput.action.performed += SwitchSprint;
            _sprintInput.action.canceled += SwitchSprint;
        }

        private void OnDisable()
        {
            _moveAxis.action.Disable();
            _sprintInput.action.Enable();
            _sprintInput.action.performed -= SwitchSprint;
            _sprintInput.action.canceled -= SwitchSprint;
        }

        private void Start()
        {
            _currentMovementSpeed = _baseMovementSpeed;
        }

        private void Update()
        {
            var input = _moveAxis.action.ReadValue<Vector2>().normalized;
            var moveDirectionLocal = Vector3.zero;
            moveDirectionLocal.x = input.x;
            moveDirectionLocal.z = input.y;

            Vector3 moveDirectionWorld = transform.TransformDirection(moveDirectionLocal);
            _motion.x = moveDirectionWorld.x * _currentMovementSpeed;
            _motion.z = moveDirectionWorld.z * _currentMovementSpeed;

            if (!_controller.isGrounded)
                _motion.y += GRAVITY_VALUE * Time.deltaTime;

            _controller.Move(_motion * Time.deltaTime);
        }

        private void SwitchSprint(InputAction.CallbackContext ctx)
        {
            switch (ctx.phase)
            {
                case InputActionPhase.Performed:
                    _currentMovementSpeed = _baseMovementSpeed * 2f;
                    break;

                case InputActionPhase.Canceled:
                    _currentMovementSpeed = _baseMovementSpeed;
                    break;
            }
        }
    }
}
