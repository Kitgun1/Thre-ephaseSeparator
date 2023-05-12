using System;
using _Project.Scripts.Enums;
using KimicuUtilities.Coroutines;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Systems.Player
{
    [RequireComponent(typeof(PlayerPhysics))]
    public class PlayerRotate : MonoBehaviour
    {
        [BoxGroup("Properties:"), LabelText("Speed horizontal rotate"), SerializeField, Range(60F, 180F)]
        private float _horizontalSpeed = 60F;

        [BoxGroup("Properties:"), LabelText("Speed vertical rotate"), SerializeField, Range(60F, 180F)]
        private float _verticalSpeed = 60F;

        [BoxGroup("Dependencies:"), LabelText("Cinemachine Virtual Camera"), SerializeField]
        private CinemachineVirtualCamera _virtualCamera;

        private PlayerPhysics _physics;
        private CinemachinePOV _pov;
        private CinemachineOrbitalTransposer _orbitalTransposer;

        private KiCoroutine _horizontalRoutine;
        private KiCoroutine _verticalRoutine;

        private Quaternion _currentQuaternion;
        private float _currentHorizontalValue;
        private float _currentVerticalValue;

        private Vector2 _direction;

        public void SetDirection(Vector2 value) => _direction = value;

        private void Start()
        {
            _physics = GetComponent<PlayerPhysics>();

            _pov = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
            _orbitalTransposer = _virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

            _horizontalRoutine = new KiCoroutine();
            _verticalRoutine = new KiCoroutine();
        }

        private void FixedUpdate()
        {
            Rotation(new Vector2(_direction.x, _direction.y));
        }

        private void Rotation(Vector2 direction)
        {
            _pov.m_HorizontalAxis.Value += direction.x * _horizontalSpeed * Time.deltaTime;
            _pov.m_VerticalAxis.Value += direction.y * _verticalSpeed * Time.deltaTime;

            Quaternion newRotation = Quaternion.Euler(new Vector3(
                Mathf.Clamp(_pov.m_VerticalAxis.Value, _pov.m_VerticalAxis.m_MinValue, _pov.m_VerticalAxis.m_MaxValue),
                Mathf.Clamp(_pov.m_HorizontalAxis.Value, _pov.m_HorizontalAxis.m_MinValue, _pov.m_HorizontalAxis.m_MaxValue),
                0));
            _physics.Rigidbody.rotation = newRotation;
        }
    }
}