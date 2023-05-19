using System.Collections.Generic;
using _Project.Scripts.Systems.Interact;
using KimicuUtilities.Attributes;
using UnityEngine;

namespace _Project.Scripts.Systems.Quest
{
    public class Examination : MonoBehaviour
    {
        [RequireInterface(typeof(ISwitchable)), SerializeField]
        private List<GameObject> _switchableObjects;

        private List<ISwitchable> _switchables = new List<ISwitchable>();

        public bool IsUnderway = false;

        private void Awake()
        {
            foreach (GameObject gameObject in _switchableObjects)
            {
                _switchables.Add(gameObject.GetComponent<ISwitchable>());
            }
        }

        public void StartExam()
        {
            IsUnderway = true;
            foreach (ISwitchable switchable in _switchables)
            {
                switchable.TurnOff();
            }
        }
    }
}