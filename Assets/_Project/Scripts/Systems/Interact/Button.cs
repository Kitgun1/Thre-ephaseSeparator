using DG.Tweening;
using KimicuUtilities.Coroutines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Systems.Interact
{
    public class Button : InteractObject
    {
        [BoxGroup("Dependency"), LabelText("Button Object"), SerializeField]
        private Transform _buttonObject;

        [BoxGroup("Dependency"), LabelText("Pressed Point"), SerializeField]
        private Transform _pressedPoint;

        [BoxGroup("Dependency"), LabelText("Unpressed Point"), SerializeField]
        private Transform _unpressedPoint;

        [BoxGroup("Property"), LabelText("Duration Animation Press"), Range(0.1F, 0.5F), SerializeField]
        private float _durationAnimation = 0.1F;

        [BoxGroup("Property"), LabelText("Duration Pressed"), Range(0.1F, 5F), SerializeField]
        private float _durationPressed = 0.1F;

        private KiCoroutine _routine;

        private void Awake()
        {
            _routine = new KiCoroutine();
        }

        public override void Switch()
        {
            Press(!State);
            base.Switch();
        }

        public override void TurnOff()
        {
            if (!State) return;

            Press(false);
            base.TurnOff();
        }

        public override void TurnOn()
        {
            if (State) return;

            Press(true);
            base.TurnOn();
        }

        private void Press(bool pressed = false)
        {
            Vector3 target = _unpressedPoint.position;
            if (pressed) target = _pressedPoint.position;

            _buttonObject.DOMove(target, _durationAnimation)
                .OnComplete(() => _routine.StartRoutineDelay(_durationPressed, onEnd: Switch));
        }
    }
}