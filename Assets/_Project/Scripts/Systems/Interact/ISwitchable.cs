using System;

namespace _Project.Scripts.Systems.Interact
{
    public interface ISwitchable
    {
        public event Action OnEnabled;
        public event Action OnDisabled;
        
        public void Switch();

        public void TurnOff();

        public void TurnOn();
    }
}