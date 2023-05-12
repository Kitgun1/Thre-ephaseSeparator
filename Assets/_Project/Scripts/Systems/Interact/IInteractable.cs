using System;

namespace _Project.Scripts.Systems.Interact
{
    public interface IInteractable
    {
        public event Action<bool> OnInteracted;
    }
}