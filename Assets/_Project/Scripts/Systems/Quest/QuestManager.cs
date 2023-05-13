using System.Collections.Generic;
using _Project.Scripts.Systems.UI;
using UnityEngine;

namespace _Project.Scripts.Systems.Quest
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private UIQuestDisplay _display;
        [SerializeField] private List<Quest> _quests = new List<Quest>();

        private readonly List<IQuest> _questsCompleted = new List<IQuest>(16);

        private bool IsAllCompleted => _questsCompleted.Count == _quests.Count;

        private void Start()
        {
            foreach (Quest questInfo in _quests)
            {
                questInfo.InitListener();
                questInfo.OnCompleteAll += OnCompleted;
                questInfo.OnUncomplete += OnUncompleted;
                _display.AddQuests(questInfo.QuestInfo);
            }
        }

        private void OnCompleted(IQuest quest)
        {
            _questsCompleted.Add(quest);
            quest.OnCompleteAll -= OnCompleted;
            if (IsAllCompleted)
            {
                // Все квесты выполнены
            }
        }

        private void OnUncompleted(IQuest quest)
        {
            _questsCompleted.Remove(quest);
            quest.OnCompleteAll -= OnCompleted;
        }
    }
}