using System;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using Cinemachine;
using KimicuUtilities.Coroutines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Systems.Player
{
    public class PlayerScaling : MonoBehaviour, IScalable
    {
        [BoxGroup("Settings:"), LabelText("Enable"), SerializeField]
        private bool _available = false;

        [BoxGroup("Properties:"), LabelText("Duration lerp"), SerializeField, Range(0.1F, 1.5F)]
        private float _duration = 0.3f;

        [BoxGroup("Properties:"), LabelText("Default zoom"), SerializeField]
        private ScaleType _defaultZoom = ScaleType.Normal;

        [BoxGroup("Dependencies:"), LabelText("Cinemachine Virtual Camera"), SerializeField]
        private CinemachineVirtualCamera _virtualCamera;

        private CinemachineTransposer _transposer;
        private KiCoroutine _lerpOffsetRoutine;

        private Vector3 _followOffset;

        public ScaleType ScaleType { get; private set; }

        private void Awake()
        {
            _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

            _lerpOffsetRoutine = new KiCoroutine();

            ScaleType = _defaultZoom;
            SetScale(ScaleType);
            StartRoutine(new Vector3(0F, 0.5F, -4F));
        }

        public void ChangeScale(int value = 0)
        {
            if (!_available) return;
            value = Mathf.Clamp(value, -1, 1);

            if ((int)ScaleType + value <= (int)ScaleType.SuperZoom &&
                (int)ScaleType + value >= (int)ScaleType.SuperDecrease && value != 0)
            {
                ScaleType += value;
            }

            SetScale(ScaleType);
        }

        private void SetScale(ScaleType scaleType)
        {
            if (!_available) return;
            _followOffset = _transposer.m_FollowOffset;
            switch (scaleType)
            {
                case ScaleType.SuperDecrease:
                    StartRoutine(new Vector3(0F, 2F, -4F));
                    break;
                case ScaleType.Decrease:
                    StartRoutine(new Vector3(0F, 1.5F, -4F));
                    break;
                case ScaleType.Normal:
                    StartRoutine(new Vector3(0F, 1F, -4F));
                    break;
                case ScaleType.Zoom:
                    StartRoutine(new Vector3(0F, 0.75F, -4F));
                    break;
                case ScaleType.SuperZoom:
                    StartRoutine(new Vector3(0F, 0.5F, -4F));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void StartRoutine(Vector3 target)
        {
            _lerpOffsetRoutine.StopRoutine();

            _lerpOffsetRoutine.StartRoutineLoop(
                _duration,
                onPerform: time => _transposer.m_FollowOffset = Vector3.Lerp(_followOffset, target, time)
            );
        }

        private void StopRoutine()
        {
            _lerpOffsetRoutine.StopRoutine();
        }
    }
}