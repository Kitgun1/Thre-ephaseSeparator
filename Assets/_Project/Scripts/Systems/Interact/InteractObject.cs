using System;
using _Project.Scripts.Systems.Quest;
using UnityEngine;

namespace _Project.Scripts.Systems.Interact
{
    public abstract class InteractObject : MonoBehaviour, IInteractable, ISwitchable, ITaskQuest
    {
        protected bool State = false;

        public event Action<bool> OnInteracted;

        public event Action OnEnabled;
        public event Action OnDisabled;

        public event Action<ITaskQuest> OnComplete;
        public event Action<ITaskQuest> OnUncomplete;

        public virtual void Switch()
        {
            State = !State;
            OnInteracted?.Invoke(State);
            if (State)
            {
                OnEnabled?.Invoke();
                OnComplete?.Invoke(this);
            }
            else
            {
                OnDisabled?.Invoke();
                OnUncomplete?.Invoke(this);
            }
        }

        public virtual void TurnOff()
        {
            State = false;
            OnInteracted?.Invoke(State);
            OnDisabled?.Invoke();
            OnUncomplete?.Invoke(this);
        }

        public virtual void TurnOn()
        {
            State = true;
            OnInteracted?.Invoke(State);
            OnEnabled?.Invoke();
            OnComplete?.Invoke(this);
        }
    }
}