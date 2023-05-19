using System;
using System.Collections.Generic;

namespace _Project.Scripts.Systems.Quest
{
    public interface IQuest
    {
        public event Action<IQuest> OnCompleteAll;
        public event Action<IQuest> OnComplete;
        public event Action<IQuest> OnUncomplete;

        public void InitListener();
    }
}