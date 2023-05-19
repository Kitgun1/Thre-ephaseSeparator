using System;
using System.Collections;
using KimicuUtilities.Math;
using UnityEngine;
using UnityEngine.Events;

namespace KimicuUtilities.Coroutines
{
    public class KiCoroutine
    {
        private Coroutine _routine;

        #region Routine Delay

        public void StartRoutineDelay(float delay,
            UnityAction onStart = null, UnityAction<float> onPerform = null, UnityAction onEnd = null)
        {
            if (_routine != null) StopRoutine();

            _routine = Coroutines.StartRoutine(RoutineDelay(delay, onStart, onPerform, onEnd));
        }

        public bool TryStartRoutineDelay(float delay,
            UnityAction onStart = null, UnityAction<float> onPerform = null, UnityAction onEnd = null)
        {
            if (_routine != null) return false;

            _routine = Coroutines.StartRoutine(RoutineDelay(delay, onStart, onPerform, onEnd));
            return true;
        }

        private IEnumerator RoutineDelay(float delay,
            UnityAction onStart = null, UnityAction<float> onPerform = null, UnityAction onEnd = null)
        {
            float time = 0F;
            onStart?.Invoke();
            while (true)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                time += Time.deltaTime;
                float value = KiMath.CalculatePercentage(time, delay) / 100;
                onPerform?.Invoke(value);

                if (time >= delay) break;
            }

            onEnd?.Invoke();
            StopRoutine();
        }

        #endregion

        #region Routine Loop

        public void StartRoutineLoop(float duration, float speed = 1F,
            UnityAction<float> onStart = null, UnityAction<float> onPerform = null, UnityAction<float> onEnd = null)
        {
            if (_routine != null) StopRoutine();

            _routine = Coroutines.StartRoutine(RoutineLoop(duration, speed, onStart, onPerform, onEnd));
        }

        public bool TryStartRoutineLoop(float duration, float speed = 1F,
            UnityAction<float> onStart = null, UnityAction<float> onPerform = null, UnityAction<float> onEnd = null)
        {
            if (_routine != null) return false;

            _routine = Coroutines.StartRoutine(RoutineLoop(duration, speed, onStart, onPerform, onEnd));
            return true;
        }

        private IEnumerator RoutineLoop(float duration, float speed = 1F,
            UnityAction<float> onStart = null, UnityAction<float> onPerform = null, UnityAction<float> onEnd = null)
        {
            float time = 0;
            float value = 0;
            onStart?.Invoke(value);
            while (true)
            {
                yield return new WaitForSeconds(Time.deltaTime * speed);
                time += Time.deltaTime * speed;
                value = (float)KiMath.CalculatePercentage(time, duration) / 100;
                onPerform?.Invoke(value);

                if (time >= duration) break;
            }

            onEnd?.Invoke(value);
            StopRoutine();
        }

        #endregion

        public void StopRoutine()
        {
            Coroutines.StopRoutine(_routine);
            _routine = null;
        }
    }
}