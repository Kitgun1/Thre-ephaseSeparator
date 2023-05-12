using System;

namespace _Project.Scripts.Systems.Quest
{
    public interface ITaskQuest
    {
        public event Action<ITaskQuest> OnComplete;
        public event Action<ITaskQuest> OnUncomplete;
    }
}