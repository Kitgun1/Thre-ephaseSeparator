using _Project.Scripts.Enums;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Systems.Interact
{
    public class Valve : InteractObject
    {
        [BoxGroup("Dependency"), LabelText("Valve Object"), SerializeField]
        private Transform _valveObject;

        [BoxGroup("Property"), LabelText("Duration Turn"), Range(0.1F, 1F), SerializeField]
        private float _duration = 0.1F;

        [BoxGroup("Property"), LabelText("Axis Direction"), SerializeField]
        private Vector3Int _axis;

        private bool _available = true;

        public override void Switch()
        {
            Rotate(State ? _axis * -1 : _axis, base.Switch);
        }

        public override void TurnOff()
        {
            if (!State) return;
            Rotate(_axis, base.TurnOff);
        }

        public override void TurnOn()
        {
            if (State) return;
            Rotate(_axis * -1, base.TurnOn);
        }

        private void Rotate(Vector3 direction, TweenCallback callback)
        {
            if (!_available) return;

            _valveObject.DOLocalRotate(direction * 360, _duration, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine)
                .OnStart(() => _available = false)
                .OnComplete(() =>
                {
                    callback?.Invoke();
                    _available = true;
                });
        }
    }
}