using _Project.Scripts.Enums;
using _Project.Scripts.Systems.UI;
using NaughtyAttributes;
using UnityEngine;

namespace _Project.Scripts.Systems.Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerScaling), typeof(PlayerRotate))]
    public class PlayerInput : MonoBehaviour
    {
        [Layer, SerializeField] private int _layerInteract;

        private Input _input;
        private PlayerMovement _movement;
        private PlayerScaling _scaling;
        private PlayerRotate _rotate;
        private PlayerInteracter _playerInteracter;
        private UIWindows _ui;

        private Camera _camera;

        #region Unity Methods

        private void Awake()
        {
            _camera = Camera.main;
            Cursor.lockState = CursorLockMode.Confined;

            _playerInteracter = new PlayerInteracter(_camera);
            _movement = GetComponent<PlayerMovement>();
            _scaling = GetComponent<PlayerScaling>();
            _rotate = GetComponent<PlayerRotate>();
            _ui = FindObjectOfType<UIWindows>();
            _input = new Input();
        }

        private void Start()
        {
            InputInitStarted();
            InputInitPerformed();
            InputInitCanceled();

            _input.CameraRotation.ResetYAxisRotation.Disable();
            _input.CameraRotation.ResetXAxisRotation.Disable();
            _input.CameraRotation.Reset2DRotation.Disable();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        #endregion

        #region Methods

        private void InputInitStarted()
        {
            // Camera Rotation
            _input.CameraRotation.ResetXAxisRotation.started += ctx =>
            {
                //_rotate.LerpRotationToDefault(AxisMode.AxisY, 1);
            };
            _input.CameraRotation.ResetYAxisRotation.started += ctx =>
            {
                //_rotate.LerpRotationToDefault(AxisMode.AxisX, 1);
            };
            _input.CameraRotation.Reset2DRotation.started += ctx =>
            {
                //_rotate.LerpRotationToDefault(AxisMode.Axis2D, 1);
            };

            // Other
            _input.Other.CameraInspectionMode.started += ctx =>
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            };
        }

        private void InputInitPerformed()
        {
            // Scaling
            _input.Scaling.Zoom.performed += ctx => { _scaling.ChangeScale(1); };
            _input.Scaling.Decrease.performed += ctx => { _scaling.ChangeScale(-1); };
            _input.Scaling.ScrollZoom.performed += ctx =>
            {
                int scrollDirection = Mathf.RoundToInt(ctx.ReadValue<float>());
                _scaling.ChangeScale(scrollDirection);
            };

            // Camera Rotation
            _input.CameraRotation.Rotation2D.performed += ctx => { _rotate.SetDirection(ctx.ReadValue<Vector2>()); };

            // Movement
            _input.Move.Movement.performed += ctx =>
            {
                Vector2 moveDirection = ctx.ReadValue<Vector2>();
                _movement.SetDirection(moveDirection);
                _movement.SetSpeed((int)_scaling.ScaleType);
            };

            // Interact
            _input.UI.SwitchTips.performed += ctx => { _ui.SwitchTip(UIItem.Tip); };

            _input.Interact.PositionClick.performed += ctx =>
            {
                _playerInteracter.TryInteractObject(ctx.ReadValue<Vector2>());
            };
        }

        private void InputInitCanceled()
        {
            // Camera Rotation
            _input.CameraRotation.Rotation2D.canceled += ctx => { _rotate.SetDirection(ctx.ReadValue<Vector3>()); };

            // Movement
            _input.Move.Movement.canceled += _ =>
            {
                _movement.SetDirection(Vector2.zero);
                _movement.SetSpeed((int)_scaling.ScaleType);
            };

            // Other
            _input.Other.CameraInspectionMode.canceled += _ =>
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            };
        }

        #endregion
    }
}