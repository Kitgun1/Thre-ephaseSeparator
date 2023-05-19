using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Systems.Quest
{
    public class Quest : MonoBehaviour, IQuest
    {
        [SerializeField] private QuestInfo _questInfo;

        private readonly List<ITaskQuest> _taskCompleted = new List<ITaskQuest>(16);

        public event Action<IQuest> OnCompleteAll;
        public event Action<IQuest> OnComplete;
        public event Action<IQuest> OnUncomplete;

        private bool IsAllCompleted => _taskCompleted.Count == _questInfo.Tasks.Count;
        public QuestInfo QuestInfo => _questInfo;

        private void Awake()
        {
            _questInfo.Quest = this;
        }

        public void InitListener()
        {
            _questInfo.Quest = this;
            _taskCompleted.Clear();
            foreach (TaskInfo taskInfo in _questInfo.Tasks)
            {
                taskInfo.Task.OnComplete += OnCompleted;
                taskInfo.Task.OnUncomplete += OnUncompleted;
            }
        }

        private void OnCompleted(ITaskQuest quest)
        {
            _taskCompleted.Add(quest);
            OnComplete?.Invoke(this);
            if (IsAllCompleted)
            {
                OnCompleteAll?.Invoke(this);
            }

            //quest.OnComplete -= OnCompleted;
        }

        private void OnUncompleted(ITaskQuest quest)
        {
            if (IsAllCompleted)
            {
                OnUncomplete?.Invoke(this);
            }

            _taskCompleted.Remove(quest);

            //quest.OnUncomplete -= OnUncompleted;
        }
    }
}